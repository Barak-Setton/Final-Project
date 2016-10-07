using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class digitalSpeedometer : MonoBehaviour {

    public GameObject vehical;
    public Text speedometer;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        speedometer.text = (3*(int)vehical.GetComponent<Rigidbody>().velocity.magnitude).ToString();
    }
}
