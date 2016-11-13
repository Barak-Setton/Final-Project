using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class NetworkUserControllerScript : NetworkBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
       if (!isLocalPlayer)
        {
            return;
        }


        float breaks = 0;
        float boost = 0;
        float jump = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            breaks = 1;
        }

        if (Input.GetKey("b"))
        {
            // boost
            boost = 1;
        }
        else
        {
            boost = 0;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            // jump
            jump = 1;
        }
        else
        {
            jump = 0;
        }

        if (gameObject.tag == "ShipPlayer")
        {
            GetComponent<ThrusterController>().Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), breaks, boost, jump);
        }
        else if (gameObject.tag == "CarPlayer")
        {
            GetComponent<groundCarScript>().Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), breaks, boost, jump);
        }
    }
}
