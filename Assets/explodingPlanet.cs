using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class explodingPlanet : MonoBehaviour {

	public int explosionTime = 5;
	public float elapsedTime = 0;
	public int newSceneTime = 10;
	public float rotateSpeed = 25.0f;
	public ParticleSystem explosion;
	public GameObject planet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;

		// rotate the planet
		transform.Rotate(transform.up * rotateSpeed * Time.deltaTime);

		if (elapsedTime == explosionTime) {
			explosion.Play ();
			Destroy (this);

		}

		if (elapsedTime >= newSceneTime) {
			SceneManager.LoadScene (5);
		}
			
	}
}
