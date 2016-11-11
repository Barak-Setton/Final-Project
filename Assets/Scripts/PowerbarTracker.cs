using UnityEngine;
using System.Collections;

public class PowerbarTracker : MonoBehaviour {
	public float crashRange = 1f;
	public float edgeRange = 2f;
	public int maxPower = 100;

	public int power = 0;
	private bool frontHit = false;
	private bool nearEdge = false;

	private Transform playerTransform;
	// Use this for initialization
	void Start () {
		playerTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Vector3 rayOrigin = playerTransform.position;
		if (Physics.Raycast (rayOrigin, playerTransform.forward, out hit, crashRange)) {
			if (hit.collider.tag == "ShipPlayer" && power < maxPower) {				
				power++;
			} else if (hit.collider.tag == "CarPlayer" && power < maxPower) {
				power++;
			}
		} 
		if (Physics.Raycast (rayOrigin, playerTransform.right, out hit, edgeRange)) {
			if (hit.collider.tag == "Wall" && power < maxPower) {				
				power++;
			} 
		} 
		if (Physics.Raycast (rayOrigin, playerTransform.right * 180f, out hit, edgeRange)) {
			if (hit.collider.tag == "Wall" && power < maxPower) {				
				power++;
			} 
		}

		if (Physics.Raycast (rayOrigin, playerTransform.up * 180f, out hit, crashRange)) {
			if (hit.collider.tag == "Wall" && power < maxPower) {				
				power++;
			} 
		} 
	}

	bool hasPower(){
		return power > 0;
	}

	void usePower(){
		power--;
	}



}
