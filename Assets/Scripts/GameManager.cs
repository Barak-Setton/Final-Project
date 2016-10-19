using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Canvas gameOverCanvas;
	public GameObject player1;
	public GameObject player2;
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
