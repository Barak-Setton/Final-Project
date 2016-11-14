using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityEngine.Networking;

public class NetworkGameManager : NetworkBehaviour {

	// Instance of Game Manager to access
	public static NetworkGameManager managerController;

	//public GUIText countdownText;
	[SyncVar] public Image one;
	[SyncVar] public Image two;
	[SyncVar] public Image three;
	[SyncVar] public Image GO;
	[SyncVar] private int currentCount = 3;
	[SyncVar] public bool instantiated = false;
	[SyncVar] public bool instantiatedTwo = false;
	[SyncVar] private float time = 0;
	// carsa
	public GameObject car;
	public GameObject ship;
	public GameObject networkCar;
	public GameObject networkShip;
	public GameObject carAI;
	public GameObject shipAI;

	// spawnLocations
	public Transform spawnPointPlayer1;
	public Transform spawnPointPlayer2;

	// way point for AI
	public WaypointCircuit circuit;

	// players
	private GameObject player1;
	private GameObject player2;
	private GameObject[] players;
	[SyncVar] int numPlayers;

	//hud elements
	public GameObject digitalSpeed;
	public GameObject analogSpeed;
	public GameObject powerBar;

	private GameObject transferData;

	// Handle Game Over
	[SyncVar] public Canvas hudCanvas;
	[SyncVar] public Canvas countDownCanvas;
	[SyncVar] public Canvas gameOverCanvas;
	[SyncVar] public int counter;

	public GameObject smoothCamera;
	[SyncVar] public StateType state;

	// audio sources
	[SyncVar] AudioSource oneA;
	[SyncVar] AudioSource twoA;
	[SyncVar] AudioSource threeA;
	[SyncVar] AudioSource goA;
	[SyncVar] AudioSource backgroundMuisc;

	//set the game state externally
	public void SetState(StateType gameState)
	{
		this.state = gameState;

	}

	// StateList, state control the current section of gameplay and function accordingly
	public enum StateType
	{
		DEFAULT,
		START,
		GAMEPLAY,
		ENDGAME
	};




	// Use this for initialization
	public override void OnStartServer () {

		// manage audio files
		AudioSource[] audios = GetComponents<AudioSource>();
		oneA = audios [3];
		twoA = audios [1];
		threeA = audios [2];
		goA = audios [0];
		backgroundMuisc = audios [audios.Length - 1];


		//set the instance of this object
		managerController = this;
		hudCanvas.enabled = false;
		gameOverCanvas.enabled = false;
		countDownCanvas.enabled = false;
		one.enabled = false;
		two.enabled = false;
		three.enabled = false;
		GO.enabled = false;
		state = StateType.START;
	}

	// Update is called once per frame
	void Update () {

		//switch statement acts as determined by statelist
		switch (state) {

		case StateType.START:
			// Choose Canvas
			hudCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			countDownCanvas.enabled = true;

			if (backgroundMuisc.isPlaying) {
				instantiatedTwo = false;
				backgroundMuisc.Stop();
			}

			if (!instantiated) {
				if (TransferData.instance.multiplayerCheck ) { // Multiplayer
					
					if (isLocalPlayer)
					{
						return;
					}
					print("Hello");
					numPlayers = players.Length;
					if (players == null || players.Length < 2) {
						players = GameObject.FindGameObjectsWithTag ("Player");
						print (players.Length);
					}
					foreach (GameObject player in players) {
						player.GetComponent<NetworkUserControllerScript> ().enabled = false;
					}
					if (players.Length == 2) {
						print (players.Length);
						for (currentCount = 3; currentCount > -1; currentCount--) {
							if (currentCount == 3) {
								one.enabled = false;
								two.enabled = false;
								three.enabled = true;
								GO.enabled = false;
								oneA.Play ();
								while (time < 1.0f) {
									time += Time.deltaTime;
								}
								time = 0f;
							}
							else if (currentCount == 2){
								two.enabled = true;
								one.enabled = false;
								three.enabled = false;
								GO.enabled = false;
								twoA.Play ();
								while (time < 1.0f) {
									time += Time.deltaTime;
								}
								time = 0f;
							}
							else if (currentCount == 1){
								one.enabled = true;
								two.enabled = false;
								three.enabled = false;
								GO.enabled = false;
								threeA.Play ();
								while (time < 0.6f) {
									time += Time.deltaTime;
								}
								time = 0f;
								goA.Play ();
								while (time < 0.4f) {
									time += Time.deltaTime;
								}
								time = 0f;
							}
							else {
								two.enabled = false;
								one.enabled = false;
								three.enabled = false;
								GO.enabled = true;
								while (time < 1.5f) {
									time += Time.deltaTime;
								}
								time = 0f;
							}
						}
						currentCount = 3;
						instantiated = false;
						this.SetState (StateType.GAMEPLAY);
					}

				}
				else
				{  // Singleplayer
					if (TransferData.instance.shipID) {
						// instantiating the Ship and renaming
						player1 = ship;
						player1 = (GameObject)Instantiate (player1, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
						player1.name = "AirshipC";
						// stop user input until gameplay
						player1.GetComponent<UserControllerScript>().enabled = false;	

						analogSpeed.SetActive(false);
						digitalSpeed.SetActive(true);

						digitalSpeed.GetComponent<digitalSpeedometer> ().vehical = player1;
						digitalSpeed.GetComponent<digitalSpeedometerNetwork>().enabled = false;


						// activating AI
						player2 = carAI;
						player2 = (GameObject)Instantiate(player2, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
						player2.GetComponent<WaypointProgressTracker>().setCircuit(circuit);
						// stop AI input until gameplay
						player2.GetComponent<WaypointProgressTracker>().enabled = false;	

					} else if (!TransferData.instance.shipID) {
						// instantiating the car and renaming
						player1 = car;
						player1 = (GameObject)Instantiate (player1, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
						player1.name = "groundCar";
						// stop user input until gameplay
						player1.GetComponent<UserControllerScript>().enabled = false;	

						analogSpeed.SetActive(true);
						digitalSpeed.SetActive(false);

						analogSpeed.GetComponent<analogSpeedometer> ().vehical = player1;
						analogSpeed.GetComponent<analogSpeedometerNetwork>().enabled = false;

						powerBar.GetComponent<PowerBarNetwork>().enabled = false;
						powerBar.GetComponent<PowerBar>().setPlayer(player1);

						// activating AI
						player2 = shipAI;
						player2 = (GameObject)Instantiate(player2, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
						player2.GetComponent<WaypointProgressTracker>().setCircuit(circuit);
						// stop AI input until gameplay
						player2.GetComponent<WaypointProgressTracker>().enabled = false;
					}

					// set powerbar
					powerBar.SetActive(true);
					powerBar.GetComponent<PowerBar>().setPlayer(player1);
					powerBar.GetComponent<PowerBarNetwork>().enabled = false;
					// setting smooth camera target
					smoothCamera.GetComponent<SmoothFollowCamera> ().target = player1.GetComponent<Transform> ();
				}
				instantiated = true;
				//StartCoroutine (CountdownFunction ());
			}
			//this.SetState (StateType.GAMEPLAY);
			break;

		case StateType.GAMEPLAY:
			// start user input at gameplay
			// oneA.Play();
			if (!instantiatedTwo && !TransferData.instance.multiplayerCheck) {
				backgroundMuisc.Play ();
				player1.GetComponent<UserControllerScript> ().enabled = true;
				// enable AI movement too
				if (TransferData.instance.shipID) {
					player2.GetComponent<WaypointProgressTracker> ().enabled = true;
				} else if (!TransferData.instance.shipID) {
					player2.GetComponent<WaypointProgressTracker> ().enabled = true;
				}
				hudCanvas.enabled = true;
				gameOverCanvas.enabled = false;
				countDownCanvas.enabled = false;
				instantiatedTwo = true;
			}
			else if (!instantiatedTwo && TransferData.instance.multiplayerCheck) {//check if coroutine is done
				if (isLocalPlayer)
					return;
				backgroundMuisc.Play ();
				foreach (GameObject player in players) {
					player.GetComponent<NetworkUserControllerScript> ().enabled = true;
				}
				instantiatedTwo = true;
			}
			break;

		case StateType.ENDGAME:

			if (backgroundMuisc.isPlaying) {
				instantiatedTwo = false;
				backgroundMuisc.Stop ();
			}
			player1.SetActive (false);
			if (!TransferData.instance.multiplayerCheck)
				player2.SetActive (false);
			counter = 0;
			hudCanvas.enabled = false;
			countDownCanvas.enabled = false;
			gameOverCanvas.enabled = true;
			break;

		default:

			break;
		}
	}

	[ClientRpc]
	void RpcCallCountdown(){
		instantiated = true;
		StartCoroutine (CountdownFunction());
	}
	// HANDLE COUNT DOWN AT START STATE
	IEnumerator CountdownFunction(){
		for (currentCount = 3; currentCount > -1; currentCount--) {
			if (currentCount == 3) {
				one.enabled = false;
				two.enabled = false;
				three.enabled = true;
				GO.enabled = false;
				oneA.Play ();
				yield return new WaitForSeconds (1.0f);
			}
			else if (currentCount == 2){
				two.enabled = true;
				one.enabled = false;
				three.enabled = false;
				GO.enabled = false;
				twoA.Play ();
				yield return new WaitForSeconds (1.0f);
			}
			else if (currentCount == 1){
				one.enabled = true;
				two.enabled = false;
				three.enabled = false;
				GO.enabled = false;
				threeA.Play ();
				yield return new WaitForSeconds (0.6f);
				goA.Play ();
				yield return new WaitForSeconds (0.4f);
			}
			else {
				two.enabled = false;
				one.enabled = false;
				three.enabled = false;
				GO.enabled = true;
				yield return new WaitForSeconds (1.50f);
			}
		}
		currentCount = 3;
		instantiated = false;
		this.SetState (StateType.GAMEPLAY);
	}
}


