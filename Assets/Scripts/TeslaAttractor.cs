using UnityEngine;
using System.Collections;

public class TeslaAttractor : MonoBehaviour {

	[Header("Attributes")]
	public float pulseRate = 0.3f;
	private float pulseCountdown = 0f;
	public float range = 20f;
	public float strength = 1000f;
	public Transform head;
	private WaitForSeconds pulseDuration = new WaitForSeconds (5.0f);
	public Rigidbody[] players;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("Attract", 0f, 0.5f);
	}

	private IEnumerator Attract(){
		foreach(Rigidbody body in players){
			if (Vector3.Distance (body.transform.position, head.position) <= range) {
				Vector3 gravityDir = (body.transform.position - head.position);
				gravityDir.Normalize ();
				//Vector3 bodyUp = body.transform.up;

				body.useGravity = false;
				body.AddForce (-gravityDir * strength);
				//Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, gravityDir) * body.rotation;
				//body.rotation = Quaternion.Slerp (body.rotation, targetRotation, 50 * Time.deltaTime);

			}
		}
		yield return pulseDuration;
		foreach(Rigidbody body in players){
			body.useGravity = true;
		}
//		foreach(Rigidbody body in players){
//			if (Vector3.Distance (body.transform.position, head.position) <= range) {
//				Vector3 gravityDir = (body.transform.position - head.position).Normalize;
//				Vector3 bodyUp = body.transform.up;
//
//
//				body. (gravityDir * strength);
//				Quaternion targetRotation = Quaternion.FromToRotation (bodyUp, gravityDir) * body.rotation;
//				body.rotation = Quaternion.Slerp (body.rotation, targetRotation, 50 * Time.deltaTime);
//
//			}
//		}

	}
	// Update is called once per frame
	void Update () {
		

		if (pulseCountdown <= 0f) {
			StartCoroutine (Attract());
			pulseCountdown = 1f / pulseRate;
		}

		pulseCountdown -= Time.deltaTime;
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
