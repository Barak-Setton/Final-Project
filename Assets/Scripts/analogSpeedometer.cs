using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class analogSpeedometer : MonoBehaviour {
    public GameObject vehical;
    public Image speedometer;

    private float zero = -134;

    // Use this for initialization
    void Start()
    {
        speedometer.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, zero);
    }

    // Update is called once per frame
    void Update()
    {
        // changing the angle of the pin image relative to ~ magnitude of the car velocity * 3 
        speedometer.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, zero - (3 * vehical.GetComponent<Rigidbody>().velocity.magnitude));
    }
}
