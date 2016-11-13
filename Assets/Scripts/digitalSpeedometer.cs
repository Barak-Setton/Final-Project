using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class digitalSpeedometer : MonoBehaviour{

    public GameObject vehical;
    public Text speedometer;


	// Update is called once per frame
	void Update () {
        // setting number of speedometer relative to ~ magintude of vehicles velocity * 3
        speedometer.text = (3*(int)vehical.GetComponent<Rigidbody>().velocity.magnitude).ToString();
    }
}
