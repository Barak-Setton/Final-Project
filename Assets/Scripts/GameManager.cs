using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

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
    public GameObject carAI;
    public GameObject shipAI;

	// spawnLocations
    public Transform spawnPointPlayer1;
    public Transform spawnPointPlayer2;
    //public GameObject spawnPlane;

	// players
    private GameObject player1;
    private GameObject player2;

	//hud elements
    public GameObject digitalSpeed;
    public GameObject analogSpeed;
    public GameObject powerBar;

    private GameObject transferData;

	// Handle Game Over
	public Canvas countDownCanvas;
	public Canvas gameOverCanvas;
	public int counter;
    
    public GameObject smoothCamera;
	public StateType state;

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

		//set the instance of this object
		managerController = this;
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
			gameOverCanvas.enabled = false;
			countDownCanvas.enabled = true;

			if (!instantiated) {
				if (TransferData.instance.multiplayerCheck) { // Multiplayer

				} else { 
					if (TransferData.instance.shipID) { // Singleplayer
						// instantiating the Ship and renaming
						player1 = ship;
						player1 = (GameObject)Instantiate (player1, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
						player1.name = "AirshipC";
						digitalSpeed.GetComponent<digitalSpeedometer> ().vehical = player1;
						analogSpeed.SetActive (false);

						// activating AI
						player2 = carAI;
						player2.SetActive (true);
					} else if (!TransferData.instance.shipID) {
						// instantiating the car and renaming
						player1 = car;
						player1 = (GameObject)Instantiate (player1, spawnPointPlayer1.position, spawnPointPlayer1.rotation);
						player1.name = "groundCar";
						analogSpeed.GetComponent<analogSpeedometer> ().vehical = player1;
						digitalSpeed.SetActive (false);
						// activating AI
						player2 = shipAI;
						player2.SetActive (true);

					}

					// set powerbar
					powerBar.GetComponent<PowerBar> ().setPlayer (player1);
					// setting smooth camera target
					smoothCamera.GetComponent<SmoothFollowCamera> ().target = player1.GetComponent<Transform> ();
				}
				instantiated = true;
				StartCoroutine (CountdownFunction ());
			}
			//this.SetState (StateType.GAMEPLAY);
			break;

		case StateType.GAMEPLAY:
			gameOverCanvas.enabled = false;
			countDownCanvas.enabled = false;
			break;

		case StateType.ENDGAME:
			
			player1.SetActive (false);
			player2.SetActive (false);
			counter = 0;
			countDownCanvas.enabled = false;
			gameOverCanvas.enabled = true;
			break;

		default:
			
			break;
		}
	}

	IEnumerator CountdownFunction(){
		for (currentCount = 3; currentCount > -1; currentCount--) {
			if (currentCount == 3) {
				one.enabled = false;
				two.enabled = false;
				three.enabled = true;
				GO.enabled = false;
				yield return new WaitForSeconds (1.5f);
			}
			else if (currentCount == 2){
				two.enabled = true;
				one.enabled = false;
				three.enabled = false;
				GO.enabled = false;
				yield return new WaitForSeconds (1.5f);
			}
			else if (currentCount == 1){
				one.enabled = true;
				two.enabled = false;
				three.enabled = false;
				GO.enabled = false;
				yield return new WaitForSeconds (1.5f);
			}
			else {
				two.enabled = false;
				one.enabled = false;
				three.enabled = false;
				GO.enabled = true;
				yield return new WaitForSeconds (1.5f);
			}
		}
		currentCount = 3;
		instantiated = false;
		this.SetState (StateType.GAMEPLAY);
	}
}
