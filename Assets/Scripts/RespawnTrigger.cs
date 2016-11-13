using UnityEngine;
using System.Collections;

public class RespawnTrigger : MonoBehaviour {

	public GameObject[] players;
	public Transform spawnPoint;
	public Transform spawnPoint2;

	// respawn based on tag
	public void OnTriggerEnter(Collider col) {
		spawnPoint = col.gameObject.GetComponentInChildren<Dumm
		if (col.tag == "Player") {
			//respawn 
			col.gameObject.transform.position = spawnPoint.position;
			col.gameObject.transform.rotation = spawnPoint.rotation;

		} else if (col.tag == "CarPlayer") {
			players [1].transform.position = spawnPoint2.position;
			players [1].transform.rotation = spawnPoint2.rotation;
		}

	}
}
