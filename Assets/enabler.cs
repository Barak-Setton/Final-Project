using UnityEngine;
using System.Collections;

public class enabler : MonoBehaviour {
	public GameObject manager;
	public GameObject respawnPlane;
	public GameObject[] checkpoints;

	// Use this for initialization
	void Start () {
		manager.SetActive(true);
		respawnPlane.SetActive(true);
		checkpoints = GameObject.FindGameObjectsWithTag ("Checkpoint");
		foreach (GameObject checkpoint in checkpoints) {
			checkpoint.SetActive (true);
		}
	}
	

}
