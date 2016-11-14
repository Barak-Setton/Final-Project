using UnityEngine;
using System.Collections;

public class SmoothFollowCamera : MonoBehaviour {

	// The Player Car we are following
	public Transform target;
	// The distance in horizontal 
	public float distance = 10.0f;
	// the height 
	public float height = 5.0f;
	// Dampening
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

	void  FixedUpdate ()
	{


		// Early out if we don't have a target
		if (!target)
			return;

		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y + 3.0f;
		float wantedHeight = target.position.y + height;
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// Convert the angle into a rotation
		Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);

		// Set the position of the camera on the horizontal plane
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Set the height of the camera
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

		// Always look at the target
		transform.LookAt (target);
	}
}

