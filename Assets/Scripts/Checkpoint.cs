using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Checkpoint : NetworkBehaviour {

	public Transform spawnPoint;

	public void OnTriggerEnter(Collider col)
	{
		if (TransferData.instance.multiplayerCheck && !isLocalPlayer)
			return;
		GameManager.managerController.counter++;
		spawnPoint = col.gameObject.GetComponentInChildren<Dummy> ().GetComponent<Transform> ();
		if (col.tag == "Player") {
			spawnPoint.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			spawnPoint.transform.rotation = new Quaternion (transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		} 
	}
}
