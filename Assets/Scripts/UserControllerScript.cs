using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class UserControllerScript : NetworkBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
      /*  if (!isLocalPlayer)
        {
            return;
        }
        */

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
