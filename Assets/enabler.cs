using UnityEngine;
using System.Collections;

public class enabler : MonoBehaviour {
	public GameObject manager;
	public GameObject respawnPlane;
	public GameObject checkpointContainer;

	// Use this for initialization
	void Start () {
		manager.SetActive(true);
		respawnPlane.SetActive(true);
		checkpointContainer.SetActiveRecursively (true);
		//checkpoints = this.ch ("Checkpoint");
		//print ("checkpoint" + checkpoints[0].name);
		//foreach (GameObject checkpoint in checkpointContainer) {
		//	checkpoint.set (true);
		//	print ("checkpoint" + checkpoint.name);
		//}
	}


}
