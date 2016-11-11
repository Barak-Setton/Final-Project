using UnityEngine;
using System.Collections;

public class PowerbarTracker : MonoBehaviour {
	public float crashRange = 1f;
	public float edgeRange = 2f;
	public int maxPower = 100;

	public int power = 0;

	public Transform left;
	public Transform right;
	public Transform front;
	public Transform down;

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
		RaycastHit frontHit;
		RaycastHit leftHit;
		RaycastHit rightHit;
		RaycastHit downHit;
		Vector3 frontOrigin = front.position;
		Vector3 leftOrigin = left.position;
		Vector3 rightOrigin = right.position;
		Vector3 downOrigin = down.position;
		forwardLine.SetPosition (0, front.position);
		//downLine.SetPosition (0, playerTransform.position);
		//leftLine.SetPosition (0, playerTransform.position);
		//rightLine.SetPosition (0, playerTransform.position);
		//Physics.Raycast(
		if (Physics.Raycast (frontOrigin, (front.forward ) , out frontHit, crashRange)) {
			forwardLine.SetPosition (1, frontHit.point);
			print (power);

			if (frontHit.collider.tag == "ShipPlayer" && power < maxPower) {				
				power++;
			} else if (frontHit.collider.tag == "CarPlayer" && power < maxPower) {
				power++;
			}
		} 
		if (Physics.Raycast (leftOrigin, left.forward, out leftHit, edgeRange)) {
			//rightLine.SetPosition (1, hit.point);
			if (leftHit.collider.tag == "Wall" && power < maxPower) {				
				power++;
			} 
		} 
		if (Physics.Raycast (rightOrigin, right.forward, out rightHit, edgeRange)) {
			//leftLine.SetPosition (1, hit.point);
			if (rightHit.collider.tag == "Wall" && power < maxPower) {				
				power++;
			} 
		}

		if (Physics.Raycast (downOrigin, down.forward, out downHit, crashRange)) {
			//downLine.SetPosition (1, hit.point);
			if (downHit.collider.tag == "ShipPlayer" && power < maxPower) {				
				power++;
			} else if (downHit.collider.tag == "CarPlayer" && power < maxPower) {
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
