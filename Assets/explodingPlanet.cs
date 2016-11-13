using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class explodingPlanet : MonoBehaviour {

	public int explosionTime = 5;
	public float elapsedTime = 0;
	public int newSceneTime = 10;
	public ParticleSystem explosion;
	public GameObject planet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;

		if (elapsedTime == explosionTime) {
			explosion.Play ();
			Destroy (planet);

		}

		if (elapsedTime >= newSceneTime) {
			SceneManager.LoadScene (5);
		}
			
	}
}
