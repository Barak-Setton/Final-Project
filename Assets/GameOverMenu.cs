using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

public class GameOverMenu : MonoBehaviour {

	private GameObject[] players;

	// Use this for initialization
	void Start () {
	
	}
	
	public void LoadOnMenu()
	{
		SceneManager.LoadScene (0);
	}

	public void LoseScreen()
	{
		SceneManager.LoadScene (6);
	}

	public void RestartState()
	{
		players = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i < players.Length; i++)
			Destroy(players[i]);
		GameManager.managerController.SetState (GameManager.StateType.START);
	}
}
