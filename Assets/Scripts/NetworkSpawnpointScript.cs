using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class NetworkSpawnpointScript : NetworkBehaviour {
	public Vector3 position;
	public Quaternion rotation;
	private Transform parentTransform;

	// Use this for initialization
	public override void OnStartLocalPlayer () {
		parentTransform = GetComponent<Transform> ();
		position = parentTransform.position;
		rotation = parentTransform.rotation;
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		
		parentTransform.position = position;
		parentTransform.rotation = rotation;
	}
}
