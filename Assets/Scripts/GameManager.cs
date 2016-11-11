using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject car;
    public GameObject ship;
    public GameObject carAI;
    public GameObject shipAI;

    public GameObject player1;
    public GameObject player2;

    private GameObject transferData;

	public Canvas gameOverCanvas;
    
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
		state = StateType.START;
        if (TransferData.instance.multiplayerCheck)
        {

        }
        else { 
            //transferData = GameObject.Find("TransferData");
            if (TransferData.instance.shipID)
            {
                print("SHIP SPAWN");
                player1 = ship;
                player1.SetActive(true);
                //Instantiate(player1, new Vector3(0, 5, 0), new Quaternion(0, 0, 0, 0));

    
                player2 = carAI;
                Instantiate(player2, new Vector3(-5, 5, 0), new Quaternion(0, 0, 0, 0));
            }
            else
            {
                print("CAR SPAWN");
                player1 = car;
                player1.SetActive(true);
               // Instantiate(player1, new Vector3(5, 5, 0), new Quaternion(0, 0, 0, 0));

                shipAI.SetActive(true);
                player2 = shipAI;
                //Instantiate(player2, new Vector3(5, 5, 0), new Quaternion(0, 0, 0, 0));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	//switch statement acts as determined by statelist
		switch (state) {
		case StateType.START:
			player1.SetActive(true);
            player2.SetActive(true);
			gameOverCanvas.enabled = false;
			break;
		case StateType.GAMEPLAY:
			gameOverCanvas.enabled = false;
			break;
		case StateType.ENDGAME:
			gameOverCanvas.enabled = true;
			break;
		default:
			break;
		}
	}
}
