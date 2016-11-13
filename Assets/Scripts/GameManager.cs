using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Instance of Game Manager to access
	public static GameManager managerController;

	// carsa
    public GameObject car;
    public GameObject ship;
    public GameObject carAI;
    public GameObject shipAI;

	// spawnLocations
    public Transform spawnPointPlayer1;
    public Transform spawnPointPlayer2;

    public GameObject spawnPlane;

	// players
    private GameObject player1;
    private GameObject player2;

	//hud elements
    public GameObject digitalSpeed;
    public GameObject analogSpeed;

    public GameObject powerBar;

    private GameObject transferData;

	// Handle Game Over
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

		state = StateType.START;
		//Handle canvas Init
		gameOverCanvas.enabled = false;
        if (TransferData.instance.multiplayerCheck) // Multiplayer
        {

        }
        else { 
            if (TransferData.instance.shipID) // Singleplayer
            {
                // instantiating the Ship and renaming
                player1 = ship;
                player1 = (GameObject)Instantiate(player1, spawnPointPlayer1.position, Quaternion.identity);
                player1.name = "AirshipC";
                digitalSpeed.GetComponent<digitalSpeedometer>().vehical = player1;
                analogSpeed.SetActive(false);

                // activating AI
                player2 = carAI;
                player2.SetActive(true);
            }
            else
            {
                // instantiating the car and renaming
                player1 = car;
                player1 = (GameObject) Instantiate(player1, spawnPointPlayer1.position, Quaternion.identity);
                player1.name = "groundCar";
                analogSpeed.GetComponent<analogSpeedometer>().vehical = player1;
                digitalSpeed.SetActive(false);

                // activating AI
                player2 = shipAI;
                player2.SetActive(true);

            }

            // set powerbar
            powerBar.GetComponent<PowerBar>().setPlayer(player1);
           // print(player1);

            // setting smooth camera target
            smoothCamera.GetComponent<SmoothFollowCamera>().target = player1.GetComponent<Transform>();

            // setting spawnplane info
            spawnPlane.GetComponent<RespawnTrigger>().players[1] = player1;
            spawnPlane.GetComponent<RespawnTrigger>().players[0] = player2;

        }
    }
	
	// Update is called once per frame
	void Update () {
	
	//switch statement acts as determined by statelist
		switch (state) {
		case StateType.START:
			//player1.SetActive(true);
            //player2.SetActive(true);
			gameOverCanvas.enabled = false;
			break;
		case StateType.GAMEPLAY:
			gameOverCanvas.enabled = false;
			break;
		case StateType.ENDGAME:
			player1.SetActive (false);
			player2.SetActive (false);
			counter = 0;
			gameOverCanvas.enabled = true;
			break;
		default:
			break;
		}
	}
}
