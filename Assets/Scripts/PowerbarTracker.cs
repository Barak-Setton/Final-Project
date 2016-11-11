using UnityEngine;
using System.Collections;

public class PowerbarTracker : MonoBehaviour {
	public float crashRange = 1f;
	public float edgeRange = 2f;
	public int maxPower = 100;

	public int power = 0;
	private bool frontHit = false;
	private bool nearEdge = false;

	private LineRenderer forwardLine;
	private LineRenderer leftLine;
	private LineRenderer rightLine;
	private LineRenderer downLine;

	private Transform playerTransform;
	// Use this for initialization
	void Start () {
		playerTransform = GetComponent<Transform> ();
		forwardLine = GetComponent<LineRenderer> ();
		//leftLine = GetComponent<LineRenderer> ();
		//rightLine = GetComponent<LineRenderer> ();
		//downLine = GetComponent<LineRenderer> ();
		forwardLine.enabled = true;
		//leftLine.enabled = true;
		//rightLine.enabled = true;
		//downLine.enabled = true;

	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Vector3 rayOrigin = playerTransform.position;
		forwardLine.SetPosition (0, playerTransform.position);
		//downLine.SetPosition (0, playerTransform.position);
		//leftLine.SetPosition (0, playerTransform.position);
		//rightLine.SetPosition (0, playerTransform.position);
		//Physics.Raycast(
		if (Physics.Raycast (rayOrigin, (-playerTransform.forward ) , out hit, crashRange)) {
			forwardLine.SetPosition (1, hit.point);
			if (hit.collider.tag == "ShipPlayer" && power < maxPower) {				
				power++;
			} else if (hit.collider.tag == "CarPlayer" && power < maxPower) {
				power++;
			}
		} 
		if (Physics.Raycast (rayOrigin, playerTransform.right, out hit, edgeRange)) {
			//rightLine.SetPosition (1, hit.point);
			if (hit.collider.tag == "Wall" && power < maxPower) {				
				power++;
			} 
		} 
		if (Physics.Raycast (rayOrigin, playerTransform.right * 180f, out hit, edgeRange)) {
			//leftLine.SetPosition (1, hit.point);
			if (hit.collider.tag == "Wall" && power < maxPower) {				
				power++;
			} 
		}

		if (Physics.Raycast (rayOrigin, playerTransform.up * 180f, out hit, crashRange)) {
			//downLine.SetPosition (1, hit.point);
			if (hit.collider.tag == "ShipPlayer" && power < maxPower) {				
				power++;
			} else if (hit.collider.tag == "CarPlayer" && power < maxPower) {
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
