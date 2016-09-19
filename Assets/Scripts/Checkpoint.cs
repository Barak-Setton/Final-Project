using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public Transform spawnPoint;

	public void OnTriggerEnter(Collider col)
	{
		if (col.tag == "ShipPlayer" || col.tag == "CarPlayer") {
			spawnPoint.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		}
	}
}
