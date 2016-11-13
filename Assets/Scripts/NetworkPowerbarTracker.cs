using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPowerbarTracker : NetworkBehaviour {

	public float crashRange = 1f;
	public float edgeRange = 2f;
	public int maxPower = 100;

	public float power = 0;

	public Transform left;
	public Transform right;
	public Transform front;
	public Transform down;

	private Transform playerTransform;
	// Use this for initialization
	public override void OnStartLocalPlayer () {
		playerTransform = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		
		RaycastHit frontHit;
		RaycastHit leftHit;
		RaycastHit rightHit;
		RaycastHit downHit;
		Vector3 frontOrigin = front.position;
		Vector3 leftOrigin = left.position;
		Vector3 rightOrigin = right.position;
		Vector3 downOrigin = down.position;
		//Physics.Raycast(
		if (Physics.Raycast (frontOrigin, (front.forward ) , out frontHit, crashRange)) {
			if (frontHit.collider.tag == "Player" && power < maxPower) {				
				power++;
			} 
		} 
		if (Physics.Raycast (leftOrigin, left.forward, out leftHit, edgeRange)) {
			if (leftHit.collider.tag == "Wall" && power < maxPower) {				
				power++;
			} 
		} 
		if (Physics.Raycast (rightOrigin, right.forward, out rightHit, edgeRange)) {
			if (rightHit.collider.tag == "Wall" && power < maxPower) {				
				power++;
			} 
		}

		if (Physics.Raycast (downOrigin, down.forward, out downHit, crashRange)) {
			if (downHit.collider.tag == "Player" && power < maxPower) {				
				power++;
			} 
		} 
	}

	public bool hasPower(){
		return power > 0;
	}

	public void useJumpPower(){
		power--;
	}
	public void useBoostPower()
	{
		power = power - 0.2f;
	}

	public float getPower(){
		return power;
	}


}
