using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class digitalSpeedometer : NetworkBehaviour{

    public GameObject vehical;
    public Text speedometer;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        // setting number of speedometer relative to ~ magintude of vehicles velocity * 3
        speedometer.text = (3*(int)vehical.GetComponent<Rigidbody>().velocity.magnitude).ToString();
    }
}
