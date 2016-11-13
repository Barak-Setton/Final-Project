using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public Transform spawnPoint;
	public Transform spawnPoint2;

	public void OnTriggerEnter(Collider col)
	{
		GameManager.managerController.counter++;
		if (col.tag == "ShipPlayer") {
			spawnPoint.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			spawnPoint.transform.rotation = new Quaternion (transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		} else if (col.tag == "CarPlayer") {
			spawnPoint2.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			spawnPoint2.transform.rotation = new Quaternion (transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		}
	}
}
