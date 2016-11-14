using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class explodingPlanet : MonoBehaviour {

	public float awwTime;
	public float yayTime;
	bool yayed = false;
	bool awwed = false;
	public float destructionTime;
	public float celebrationTime;
	public float printerTime;
	public int explosionTime;
	public float explosionDone;
	public float celebrationDone;
	public float elapsedTime = 0;
	public int newSceneTime;
	public float rotateSpeed = 25.0f;
	public ParticleSystem explosion;
	public ParticleSystem celebration;
	public GameObject planet;
	public GameObject printer;
	bool exploded = false;
	bool sounded = false;
	bool celebrate = false;
	public GameObject particleParent;
	public GameObject celebrationParent;
	//AudioSource explosionSound;
	AudioSource aww;
	AudioSource yay;
	AudioSource[] sounds;

	public GameObject text1;
	public GameObject text2;

	// Use this for initialization
	void Start () {
		sounds = GetComponents<AudioSource> ();
		aww = sounds[0];
		yay = sounds [1];
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
			sounded = true;
		}

		if (elapsedTime >= explosionTime && !exploded) {
			explosion.Play();
			exploded = true;
		}

		if (elapsedTime >= awwTime && !awwed) {
			aww.Play ();
			awwed = true;
		}

		if (elapsedTime > explosionDone) {
			particleParent.SetActive(false);
			text2.SetActive (true);
			text1.SetActive (false);
		}

		if (elapsedTime > printerTime) {
			printer.SetActive (true);
		}

		if (elapsedTime >= celebrationTime && !celebrate) {
			celebration.Play();
			celebrate = true;
		}

		if (elapsedTime >= yayTime && !yayed) {
			yay.Play ();
			yayed = true;
		}

		if (elapsedTime > celebrationDone) {
			celebrationParent.SetActive(false);
		}

		if (elapsedTime >= newSceneTime) {
			SceneManager.LoadScene (0);
		}
			
	}
}
