using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Checkpoint : MonoBehaviour {

	public SpawnpointScript spawnPoint;

	public void OnTriggerEnter(Collider col)
	{
		spawnPoint = col.gameObject.GetComponentInChildren<SpawnpointScript> ();
		if (col.tag == "Vehicel") {
			GameManager.managerController.counter++;
			spawnPoint.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			spawnPoint.rotation = new Quaternion (transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		} 
	}

}
