using UnityEngine;
using System.Collections;

public class MultiCameraController : MonoBehaviour {
	public Camera[] cameras;
	private int currentCameraIndex;

	void Start () {
		currentCameraIndex = 0;

		//Turn all cameras off, except the first one
		for (int i=1; i<cameras.Length; i++) 
		{
			cameras[i].enabled = false;
		}

		//If any cameras were added to the controller, enable the first one
		if (cameras.Length>0)
		{
			cameras [0].enabled = (true);
		}
	}

	// Update is called once per frame
	void Update () {
		//If the c button is pressed, switch to the next camera
		if (Input.GetKeyDown(KeyCode.C))
		{
			currentCameraIndex ++;
			Debug.Log ("C button has been pressed. Switching to the next camera");
			if (currentCameraIndex < cameras.Length)
			{
				// new camera active, old camera inactive
				cameras[currentCameraIndex-1].enabled = (false);
				cameras[currentCameraIndex].enabled = (true);
			}
			else
			{
				cameras[currentCameraIndex-1].enabled = (false);
				// return to begininng
				currentCameraIndex = 0;
				cameras[currentCameraIndex].enabled = (true);
			}
		}
	}
}
