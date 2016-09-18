using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public Canvas gameOverText;

	public void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			gameOverText.enabled = true;
		}
	}
}
