using UnityEngine;
using System.Collections;

public class TransferData : MonoBehaviour {

	// instance to share data
	public static TransferData instance;

	// data to be passed between scenes
	public bool shipID;
	public bool multiplayerCheck;

	void Awake(){
		// maintains gameobject between scenes
		Object.DontDestroyOnLoad(this);
	}

	void Start(){
		instance = this;
	}
}
