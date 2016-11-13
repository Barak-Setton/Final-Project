﻿using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// when a player crosses the finish line set the gamestate to ENDGAME
	public void OnTriggerEnter(Collider col)
	{
		if ((col.tag == "ShipPlayer" || col.tag == "CarPlayer") && GameManager.managerController.counter > 20) {
			GameManager.managerController.SetState (GameManager.StateType.ENDGAME);
		}
	}
}
