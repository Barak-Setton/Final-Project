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
            if (TransferData.instance.shipID)
            {
                player1 = ship;
                player1.SetActive(true);

                player2 = carAI;
                player2.SetActive(true);
            }
            else
            {
                player1 = car;
                player1.SetActive(true);

                player2 = shipAI;
                player2.SetActive(true);
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
