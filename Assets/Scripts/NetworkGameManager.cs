using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityEngine.Networking;

public class NetworkGameManager : NetworkBehaviour {

	// Instance of Game Manager to access
	public static NetworkGameManager managerController;

	//public GUIText countdownText;
	public Image one;
	public Image two;
	public Image three;
	public Image GO;
	[SyncVar] private int currentCount = 3;
	[SyncVar] public bool instantiated = false;
	[SyncVar] public bool instantiatedTwo = false;
	[SyncVar] public bool enable = false;
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
	//public Canvas hudCanvas;
	public Canvas countDownCanvas;
	public Canvas gameOverCanvas;
	[SyncVar] public int counter;

	public GameObject smoothCamera;
	[SyncVar] public StateType state;

	// audio sources
	AudioSource oneA;
	AudioSource twoA;
	AudioSource threeA;
	AudioSource goA;
	AudioSource backgroundMuisc;

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

    //	public override void OnStartLocalPlayer () {
    //
    //		// manage audio files
    //		AudioSource[] audios = GetComponents<AudioSource>();
    //		oneA = audios [3];
    //		twoA = audios [1];
    //		threeA = audios [2];
    //		goA = audios [0];
    //		backgroundMuisc = audios [audios.Length - 1];
    //
    //
    //		//set the instance of this object
    //		managerController = this;
    //		hudCanvas.enabled = false;
    //		gameOverCanvas.enabled = false;
    //		countDownCanvas.enabled = false;
    //		one.enabled = false;
    //		two.enabled = false;
    //		three.enabled = false;
    //		GO.enabled = false;
    //		state = StateType.START;
    //	}


    // Use this for initialization


    void Start () {

		// manage audio files
		AudioSource[] audios = GetComponents<AudioSource>();
		oneA = audios [3];
		twoA = audios [1];
		threeA = audios [2];
		goA = audios [0];
		backgroundMuisc = audios [audios.Length - 1];


		//set the instance of this object
		managerController = this;
		//hudCanvas.enabled = false;
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
        if (isLocalPlayer)
        {
            return;
        }
        //switch statement acts as determined by statelist
        switch (state) {

		case StateType.START:
			// Choose Canvas
			//hudCanvas.enabled = false;
			gameOverCanvas.enabled = false;
			countDownCanvas.enabled = true;

			if (backgroundMuisc.isPlaying) {
				instantiatedTwo = false;
				backgroundMuisc.Stop ();
			}
			if (!enable) {
				players = GameObject.FindGameObjectsWithTag ("Player");
				foreach (GameObject player in players) {
					player.GetComponent<NetworkUserControllerScript> ().enabled = false;
				}
			}
			if (!instantiated && players.Length >= 2) {
				if (TransferData.instance.multiplayerCheck ) { // Multiplayer


					print (numPlayers);
					print (instantiated);
					time = 0;
					numPlayers = players.Length;
					StartCoroutine (CountdownFunction ());
					instantiated = true;
					enable = true;
				}
			}
			break;

		case StateType.GAMEPLAY:
			// start user input at gameplay
			// oneA.Play();

			if (!instantiatedTwo && TransferData.instance.multiplayerCheck) {//check if coroutine is done
				
				backgroundMuisc.Play ();
				foreach (GameObject player in players) {
					player.GetComponent<NetworkUserControllerScript>().enabled = true;
				}
				//hudCanvas.enabled = true;
				gameOverCanvas.enabled = false;
				countDownCanvas.enabled = false;
				instantiatedTwo = true;
			}
			break;

		case StateType.ENDGAME:

			if (backgroundMuisc.isPlaying) {
				instantiatedTwo = false;
				backgroundMuisc.Stop ();
			}


			foreach (GameObject player in players) {
				SetActiveRecursively(player, false);
			}
			counter = 0;
			//hudCanvas.enabled = false;
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

	[ClientRpc]
	void RpcIncrementTime(){
		time += Time.deltaTime; 
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

	// implemented deprecated setactiverecursively ourselves
	public static void SetActiveRecursively(GameObject rootObject, bool active)
	{
		rootObject.SetActive (active);

		foreach (Transform childTransform in rootObject.transform) {
			SetActiveRecursively (childTransform.gameObject, active);
		}
	}
}


