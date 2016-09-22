using UnityEngine;
using System.Collections;

public class RespawnTrigger : MonoBehaviour {

	public GameObject[] players;
	public Transform spawnPoint;
	public Transform spawnPoint2;

	// respawn based on tag
	public void OnTriggerEnter(Collider col) {
		if (col.tag == "ShipPlayer") {
			//respawn 
			players [0].transform.position = spawnPoint.position;
			players [0].transform.rotation = spawnPoint.rotation;
		} else if (col.tag == "CarPlayer") {
			players [1].transform.position = spawnPoint2.position;
			players [1].transform.rotation = spawnPoint2.rotation;
		}

	}
}
