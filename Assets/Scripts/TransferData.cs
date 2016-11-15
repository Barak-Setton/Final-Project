using UnityEngine;
using System.Collections;

public class TransferData : MonoBehaviour {

	// instance to share data
	public static TransferData instance;

	// data to be passed between scenes
	public bool shipID;
	public bool multiplayerCheck;
	public bool alreadySplash;

	void Awake(){
		// maintains gameobject between scenes
		if (instance) {
			DestroyImmediate (gameObject);
		} else {
			Object.DontDestroyOnLoad (this);
			instance = this;
		}
	}

	void Start(){
	}
}
