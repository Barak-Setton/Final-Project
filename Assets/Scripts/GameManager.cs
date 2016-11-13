using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

	// Instance of Game Manager to access
	public static GameManager managerController;

	//public GUIText countdownText;
	public Image one;
	public Image two;
	public Image three;
	public Image GO;
	private int currentCount = 3;
	public bool instantiated = false;

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

	//hud elements
    public GameObject digitalSpeed;
    public GameObject analogSpeed;
    public GameObject powerBar;

    private GameObject transferData;

	// Handle Game Over
	public Canvas hudCanvas;
	public Canvas countDownCanvas;
	public Canvas gameOverCanvas;
	public int counter;
    
    public GameObject smoothCamera;
	public StateType state;

	// audio sources
	AudioSource oneA;
	AudioSource twoA;
	AudioSource threeA;
	AudioSource goA;

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
	void Start () {
		// manage audio files
		AudioSource[] audios = GetComponents<AudioSource>();
		oneA = audios [3];
		twoA = audios [1];
		threeA = audios [2];
		goA = audios [0];


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

			if (!instantiated) {
				if (TransferData.instance.multiplayerCheck ) { // Multiplayer
					if (!isLocalPlayer)
						return;
					NetworkStartPosition instantiatedPoint = GameObject.FindObjectOfType<NetworkStartPosition> ();
					if (TransferData.instance.shipID)
						player1 = (GameObject)Instantiate (networkShip, instantiatedPoint.transform.position, instantiatedPoint.transform.rotation);
					else if (!TransferData.instance.shipID)
						player1 = (GameObject)Instantiate (networkCar, instantiatedPoint.transform.position, instantiatedPoint.transform.rotation);
					else
						print ("No vehicle selected");
					NetworkServer.Spawn (player1);
				}else { 
					if (TransferData.instance.shipID) { // Singleplayer
						    // instantiating the Ship and renaming
						    player1 = ship;
						    player1 = (GameObject)Instantiate (player1, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
						    player1.name = "AirshipC";

                            analogSpeed.SetActive(false);
                            digitalSpeed.SetActive(true);

                            digitalSpeed.GetComponent<digitalSpeedometer> ().vehical = player1;
                            digitalSpeed.GetComponent<digitalSpeedometerNetwork>().enabled = false;

                            // activating AI
                            player2 = carAI;
                            player2 = (GameObject)Instantiate(player2, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
                            player2.GetComponent<WaypointProgressTracker>().setCircuit(circuit);

					} else if (!TransferData.instance.shipID) {
						    // instantiating the car and renaming
						    player1 = car;
						    player1 = (GameObject)Instantiate (player1, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
						    player1.name = "groundCar";

                            analogSpeed.SetActive(true);
                            digitalSpeed.SetActive(false);

                            analogSpeed.GetComponent<analogSpeedometer> ().vehical = player1;
                            analogSpeed.GetComponent<analogSpeedometerNetwork>().enabled = false;
                            
						    // activating AI
						    player2 = shipAI;
                            player2 = (GameObject)Instantiate(player2, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
                            player2.GetComponent<WaypointProgressTracker>().setCircuit(circuit);
                        }

                        // set powerbar
                        powerBar.SetActive(true);
					    powerBar.GetComponent<PowerBar> ().setPlayer (player1);
                        powerBar.GetComponent<PowerBarNetwork>().enabled = false;
                        // setting smooth camera target
                        smoothCamera.GetComponent<SmoothFollowCamera> ().target = player1.GetComponent<Transform> ();
				}
				instantiated = true;
				StartCoroutine (CountdownFunction ());
			}
			//this.SetState (StateType.GAMEPLAY);
			break;

		case StateType.GAMEPLAY:
			hudCanvas.enabled = true;
			gameOverCanvas.enabled = false;
			countDownCanvas.enabled = false;
			break;

		case StateType.ENDGAME:
			
			player1.SetActive (false);
			if (!TransferData.instance.multiplayerCheck)
				player2.SetActive (false);
			counter = 0;
			countDownCanvas.enabled = false;
			gameOverCanvas.enabled = true;
			break;

		default:
			
			break;
		}
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
