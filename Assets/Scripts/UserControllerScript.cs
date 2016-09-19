using UnityEngine;
using System.Collections;

public class UserControllerScript : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {


        float breaks = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            breaks = 1;
        }


        if (gameObject.tag == "ShipPlayer")
        {
            GetComponent<ThrusterController>().Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), breaks);
        }
        else if (gameObject.tag == "CarPlayer")
        {
            GetComponent<groundCarScript>().Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), breaks);
        }
    }
}
