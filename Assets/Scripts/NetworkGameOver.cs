using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class GameOver : NetworkBehaviour {

	// when a player crosses the finish line set the gamestate to ENDGAME
	public void OnTriggerEnter(Collider col)
	{
		if (!isLocalPlayer)
			return;
		if ((col.tag == "Vehicel" || col.tag == "Player") && NetworkGameManager.managerController.counter > 20) {
			NetworkGameManager.managerController.SetState (NetworkGameManager.StateType.ENDGAME);
		}
	}
}
