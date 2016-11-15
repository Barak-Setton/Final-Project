using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class NetworkEnabler : NetworkBehaviour {
	public GameObject manager;
	public GameObject respawnPlane;
	public GameObject checkpointContainer;

	// Use this for initialization
	void OnStartLocalPlayer () {
		//manager.SetActive(true);
		respawnPlane.SetActive(true);
		SetActiveRecursively (checkpointContainer, true);
	}

	// implemented deprecated setactiverecursively ourselves
	public static void SetActiveRecursively(GameObject rootObject, bool active)
	{
		rootObject.SetActive (active);

		foreach (Transform childTransform in rootObject.transform) {
			SetActiveRecursively (childTransform.gameObject, active);
		}
	}


}
