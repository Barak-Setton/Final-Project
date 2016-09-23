﻿using UnityEngine;
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

	// Use this for initialization
	void Start () {
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
		laserLine = GetComponent<LineRenderer> ();
		//gunAudio = getComponent<audioSource>();
	}

	void UpdateTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies) {
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
