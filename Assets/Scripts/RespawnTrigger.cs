using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class RespawnTrigger : NetworkBehaviour {


	public Transform spawnPoint;

	// respawn based on tag
	public void OnTriggerEnter(Collider col) {
		if (TransferData.instance.multiplayerCheck && !isLocalPlayer)
			return;
		spawnPoint = col.gameObject.GetComponentInChildren<Dummy> ().GetComponent<Transform> ();
		if (col.tag == "Player") {
			//respawn 
			col.gameObject.transform.position = spawnPoint.position;
			col.gameObject.transform.rotation = spawnPoint.rotation;

		} 
//		else if (col.tag == "AI") {
//			col.gameObject.transform.position = spawnPoint.position;
//			col.gameObject.transform.rotation = spawnPoint.rotation;
//		}

	}
}
