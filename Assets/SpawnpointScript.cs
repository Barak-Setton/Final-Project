using UnityEngine;
using System.Collections;

public class SpawnpointScript : MonoBehaviour {
	public Vector3 position;
	public Quaternion rotation;
	private Transform parentTransform;

	// Use this for initialization
	void Start () {
		parentTransform = GetComponent<Transform> ();
		position = parentTransform.position;
		rotation = parentTransform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		parentTransform.position = position;
		parentTransform.rotation = rotation;
	}
}
