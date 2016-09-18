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
            print("user controller breaks: "+ breaks);
            
        }
        GetComponent<ThrusterController>().Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),breaks);
    }
}
