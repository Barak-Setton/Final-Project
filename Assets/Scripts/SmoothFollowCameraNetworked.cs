using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SmoothFollowCameraNetworked : NetworkBehaviour
{

    public GameObject digitalSpeedometer;
    public GameObject analogSpeedometer;
    public GameObject powerBar;

    public GameObject cam;

    // The Player Car we are following
    public Transform target;
    // The distance in horizontal 
    public float distance = 10.0f;
    // the height 
    public float height = 5.0f;
    // Dampening
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    void Start()
    {
        // enabling HUD info
        if (target.name == "AirshipNetwork(camera)(clone)")
        {
            digitalSpeedometer.SetActive(true);
        }
        else if(target.name == "groundCarNetwork(camera)(clone)")
        {
            analogSpeedometer.SetActive(true);
            analogSpeedometer.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            print("NO vehicel found");
        }

        powerBar.SetActive(true);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        // Early out if we don't have a target
        if (!target)
            return;

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y + 3.0f;
        float wantedHeight = target.position.y + height;
        float currentRotationAngle = cam.transform.eulerAngles.y;
        float currentHeight = cam.transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the horizontal plane
        cam.transform.position = target.position;
        cam.transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        cam.transform.position = new Vector3(cam.transform.position.x, currentHeight, cam.transform.position.z);

        // Always look at the target
        cam.transform.LookAt(target);
    }
}

