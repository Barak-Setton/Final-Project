using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour {
	private Transform target;

	[Header("Attributes")]
	public float fireRate = 1f;
	private float fireCountdown = 0f;
	public float range = 20f;

	[Header ("Unity Setup Fields")]
	public string enemyTag = "CarPlayer";

	public float turnSpeed = 2.5f;
	public Transform pivot;
	public Transform barrelEnd;
	//private Camera fpsCam;
	private WaitForSeconds shotDuration = new WaitForSeconds (0.07f);
	private AudioSource turretAudio;
	private LineRenderer laserLine;
	private float nextFire;

	[Header ("Player attributes")]
	public GameObject[] players;
	public Transform spawnPoint;
	public Transform spawnPoint2;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
		laserLine = GetComponent<LineRenderer> ();
		//gunAudio = getComponent<audioSource>();
	}

	void UpdateTarget(){
		//GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in players) {
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null &&	shortestDistance <= range) {
			target = nearestEnemy.transform;
		} else {
			target = null;
		}
	}

	// Update is called once per frame
	void Update () {
		if (target == null)
			return;
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp(pivot.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		pivot.rotation = Quaternion.Euler (0f, rotation.y, 0f);


		if (fireCountdown <= 0f) {
			StartCoroutine (ShotEffect());
			Shoot ();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
	}

	void Shoot(){
		print ("shoot");
		RaycastHit hit;
		Vector3 rayOrigin = barrelEnd.position;
		laserLine.SetPosition (0, barrelEnd.position);

		if (Physics.Raycast (rayOrigin, barrelEnd.forward, out hit, range)) {
			laserLine.SetPosition (1, hit.point);
			if (hit.collider.tag == "ShipPlayer") {
				//respawn 
				players [0].transform.position = spawnPoint.position;
				players [0].transform.rotation = spawnPoint.rotation;
			} else if (hit.collider.tag == "CarPlayer") {
				players [1].transform.position = spawnPoint2.position;
				players [1].transform.rotation = spawnPoint2.rotation;
			}
		} else {
			laserLine.SetPosition (1, rayOrigin + (barrelEnd.forward * range));
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}

	private IEnumerator ShotEffect(){
		
		//gunAudio.Play();
		laserLine.enabled = true;
		yield return shotDuration;
		laserLine.enabled = false;
	}
}
