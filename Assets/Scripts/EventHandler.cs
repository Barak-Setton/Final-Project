using UnityEngine;
using System.Collections;

public class EventHandler : MonoBehaviour {

	public GameObject FollowCamParent;

	void Update () {
		// activate shake

		if (Input.GetKeyDown(KeyCode.Z))
		{
			print ("BOOM");
			FollowCamParent.GetComponent<CameraShake> ().ShakeCamera (0.2f, 0.5f);
		}
	}
}