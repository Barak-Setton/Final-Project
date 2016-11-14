using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class explodingPlanet : MonoBehaviour {

	public float destructionTime;
	public float printerTime;
	public int explosionTime;
	public float explosionDone;
	public float elapsedTime = 0;
	public int newSceneTime;
	public float rotateSpeed = 25.0f;
	public ParticleSystem explosion;
	public GameObject planet;
	public GameObject printer;
	bool exploded = false;
	bool sounded = false;
	public GameObject particleParent;
	AudioSource explosionSound;

	public GameObject text1;
	public GameObject text2;

	// Use this for initialization
	void Start () {
		explosionSound = GetComponent<AudioSource> ();
		text1.SetActive (true);
		text2.SetActive (false);
		printer.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;

		// rotate the planet
		planet.transform.Rotate(planet.transform.up * rotateSpeed * Time.deltaTime);

		if (elapsedTime >= destructionTime && !sounded) {
			planet.SetActive(false);
			explosionSound.Play ();
			sounded = true;
		}

		if (elapsedTime >= explosionTime && !exploded) {
			explosion.Play();
			exploded = true;
			//explosion.Stop ();
		}

		if (elapsedTime > explosionDone) {
			particleParent.SetActive(false);
			text2.SetActive (true);
			text1.SetActive (false);
		}

		if (elapsedTime > printerTime) {
			printer.SetActive (true);
		}

		if (elapsedTime >= newSceneTime) {
			SceneManager.LoadScene (0);
		}
			
	}
}
