using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class analogSpeedometerNetwork : NetworkBehaviour
{
    public GameObject vehical;
    public Image needle;

    private float zero = -134;

    // Use this for initialization
    void Start()
    {
        needle.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        // changing the angle of the pin image relative to ~ magnitude of the car velocity * 3 
        needle.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, zero - (3 * vehical.GetComponent<Rigidbody>().velocity.magnitude));
    }
}
