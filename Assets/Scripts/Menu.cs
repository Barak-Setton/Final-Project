using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour 
{
	// Canvas objects
	public Canvas ModeCanvas;
	public Canvas SelectionCanvas;
	public Image SplashScreen;

	// Boolean to check if game is networked
	private bool isMultiplayer;
	// counter to track game time
	private float counter = 0;

	// On Awake Menu Selection is disabled
	void Awake()
	{
		SelectionCanvas.enabled = false;
	}

	// update counter and handle splash screen
	void Update()
	{
		counter += Time.deltaTime;
		if (counter >= 2) 
		{
			SplashScreen.enabled = false;
		}
	}

	// Select Multiplayer Mode
	public void MultiplayerSelectionOn()
	{
		SelectionCanvas.enabled = true;
		ModeCanvas.enabled = false;
		isMultiplayer = true;
	}

	// Select Solo Mode
	public void SoloSelectionOn()
	{
		SelectionCanvas.enabled = true;
		ModeCanvas.enabled = false;
		isMultiplayer = false;
	}

	// Start a game, determine if mulitplayer or solo
	public void LoadOn()
	{
		if (isMultiplayer) 
		{
			SceneManager.LoadScene (2);
		}
		else if (!isMultiplayer) 
		{
			SceneManager.LoadScene (1);
		}
	}

}
