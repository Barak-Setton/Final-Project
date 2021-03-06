﻿using UnityEngine;
using System.Collections;
public class UserControllerScript : MonoBehaviour
{
    public GameObject vehicel;

	private groundCarScript m_GroundCarController;
	private ThrusterController m_ThrusterController;

	void Start(){
		m_ThrusterController = vehicel.GetComponent<ThrusterController> ();
		m_GroundCarController = vehicel.GetComponent<groundCarScript> ();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
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

        if (gameObject.tag == "Player")
        {
			if (m_ThrusterController != null)
            {
                m_ThrusterController.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), breaks, boost, jump);
            }
			else if (m_GroundCarController != null)
                m_GroundCarController.Move (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), breaks, boost, jump);
			else
				print ("no controller script");
        }
    }
}
