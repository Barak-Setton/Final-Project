using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class RespawnTrigger : MonoBehaviour {


	public SpawnpointScript spawnPoint;

	// respawn based on tag
	public void OnTriggerEnter(Collider col) {
		
		spawnPoint = col.gameObject.GetComponentInChildren<SpawnpointScript> ();
		if (col.tag == "Vehicel") {
			//respawn 
			col.gameObject.transform.position = spawnPoint.position;
			col.gameObject.transform.rotation = spawnPoint.rotation;
			col.gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;

		} 
//		else if (col.tag == "AI") {
//			col.gameObject.transform.position = spawnPoint.position;
//			col.gameObject.transform.rotation = spawnPoint.rotation;
//		}

	}
}
