using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class NetworkCheckpoint : NetworkBehaviour {

	public NetworkSpawnpointScript spawnPoint;

	public void OnTriggerEnter(Collider col)
	{
//		if (TransferData.instance.multiplayerCheck && !isLocalPlayer)
//			return;

		if (col.tag == "Vehicel") {
			NetworkGameManager.managerController.counter++;
			spawnPoint = col.gameObject.GetComponentInChildren<NetworkSpawnpointScript> ();
			print ("checkpoint " + NetworkGameManager.managerController.counter + " " + col.gameObject.name);
			spawnPoint.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			spawnPoint.rotation = new Quaternion (transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		} 
	}

	public void update(){

	}
}
