using UnityEngine;
using System.Collections;

public class RespawnTrigger : MonoBehaviour {

	public GameObject player;
	public Transform spawnPoint;


	public void OnTriggerEnter(Collider col) {
		if(col.tag =="ShipPlayer" || col.tag == "CarPlayer")
        {
            //your death script
            player.transform.position = spawnPoint.position;
        } 

	}
}
