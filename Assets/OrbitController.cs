using UnityEngine;
using System.Collections;

public class OrbitController : MonoBehaviour {

	// rotation parameters
	public GameObject track;
	public Transform middle;
	public Vector3 axisOfRotation = Vector3.up;
	public Vector3 desiredPos;
	public float radius;
	public float radiusSpeed;
	public float rotationSpeed;

	void Start ()
	{
		middle = track.transform;
		transform.position = (transform.position - middle.position).normalized * radius + middle.position;
	}
	// Update is called once per frame
	void Update () {
		transform.RotateAround (middle.position, axisOfRotation, rotationSpeed * Time.deltaTime);
		desiredPos = (transform.position - middle.position).normalized * radius + middle.position;
		transform.position = Vector3.MoveTowards(transform.position, desiredPos, Time.deltaTime * radiusSpeed);
	}
}
