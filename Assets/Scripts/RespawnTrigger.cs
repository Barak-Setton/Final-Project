using UnityEngine;
using System.Collections;

public class RespawnTrigger : MonoBehaviour {


	public Transform spawnPoint;

	// respawn based on tag
	public void OnTriggerEnter(Collider col) {
		spawnPoint = col.gameObject.GetComponentInChildren<Dummy> ().GetComponent<Transform> ();
		if (col.tag == "Player") {
			//respawn 
			col.gameObject.transform.position = spawnPoint.position;
			col.gameObject.transform.rotation = spawnPoint.rotation;

		} 

	}
}
