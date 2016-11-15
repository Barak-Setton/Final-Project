using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class NetworkRespawnTrigger : NetworkBehaviour {


	public NetworkSpawnpointScript spawnPoint;

    // respawn based on tag
    public void OnTriggerEnter(Collider col)
    {
        if (TransferData.instance.multiplayerCheck && !isLocalPlayer)
            return;
        spawnPoint = col.gameObject.GetComponentInChildren<NetworkSpawnpointScript>();
        if (col.tag == "Vehicel")
        {
            //respawn 
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