﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class NetworkRespawnTrigger : NetworkBehaviour {


	public NetworkSpawnpointScript spawnPoint;

    // respawn based on tag
    public void OnTriggerEnter(Collider col)
    {
//        if (TransferData.instance.multiplayerCheck && !isLocalPlayer)
//            return;
        
        if (col.tag == "Vehicel")
        {
			print ("checkpoint " + NetworkGameManager.managerController.counter + " " + col.gameObject.name);
            //respawn 
			spawnPoint = col.gameObject.GetComponentInChildren<NetworkSpawnpointScript>();
            col.gameObject.transform.position = spawnPoint.position;
            col.gameObject.transform.rotation = spawnPoint.rotation;
            col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        }
        //		else if (col.tag == "AI") {
        //			col.gameObject.transform.position = spawnPoint.position;
        //			col.gameObject.transform.rotation = spawnPoint.rotation;
        //		}
    }
}