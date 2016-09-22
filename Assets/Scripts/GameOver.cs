using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public GameObject gameManager;

	// when a player crosses the finish line set the gamestate to ENDGAME
	public void OnTriggerEnter(Collider col)
	{
		if (col.tag == "ShipPlayer" || col.tag == "CarPlayer") {
			gameManager.GetComponent<GameManager>().SetState (GameManager.StateType.ENDGAME);
		}
	}
}
