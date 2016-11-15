using UnityEngine;
using System.Collections;

public class enabler : MonoBehaviour {
	public GameObject gameManager;
    public GameObject gameManagerNetwork;
	public GameObject respawnPlane;
	public GameObject checkpointContainer;

	// Use this for initialization
	void Start () {

        if (TransferData.instance.multiplayerCheck)
        {
            gameManager.SetActive(false);
            gameManagerNetwork.SetActive(true);
        }
        else
        {
            print("single");
            gameManagerNetwork.SetActive(false);
            gameManager.SetActive(true);
        }
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
