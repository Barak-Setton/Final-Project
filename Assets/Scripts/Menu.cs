using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

public class Menu : NetworkBehaviour 
{

    // Canvas objects
	public Canvas SelectionCanvas;
	public Canvas InstructionsCanvas;
	public Canvas SplashCanvas;
	public GameObject effects;
	public GameObject backgroundImg;
	// public GameObject transferInstance;
	//TransferData transferData;

	// Handle splash Timing
	public float elapsedTime = 0;
	public float splashedTime;
	bool splashed = false;
	bool reallySplashed = false;

	// public AudioClip showMe;

    // vehicel objetcs
    public GameObject ship;
    public GameObject car;
    public int rightTarget = 0;
    public int leftTarget = -100;

    // On Awake Menu Selection is disabled
    void Awake()
	{
    }

	void Start()
	{
		//transferData = transferInstance.GetComponent<TransferData> ();
		if (!(TransferData.instance.alreadySplash)) {
			//print ("hi" + ransferData.alreadySplash);
			SplashCanvas.enabled = true;
			SelectionCanvas.enabled = false;
			InstructionsCanvas.enabled = false;
			effects.SetActive (false);
			backgroundImg.SetActive (false);
			//transferData.alreadySplash = true;
			TransferData.instance.alreadySplash = true;
		} else {
			SplashCanvas.enabled = false;
			SelectionCanvas.enabled = true;
			InstructionsCanvas.enabled = false;
			effects.SetActive (true);
			backgroundImg.SetActive (true);
			TransferData.instance.shipID = true;
			TransferData.instance.multiplayerCheck = false;
			reallySplashed = true;
		}
		ship.SetActive(false);
		car.SetActive(false);
		scrollLeftClick ();
	}

    // car rotations
    void Update()
	{
		elapsedTime += Time.deltaTime;

		// start once splash is finished
		if (elapsedTime >= splashedTime && !splashed && !reallySplashed){
			SplashCanvas.enabled = false;
			SelectionCanvas.enabled = true;
			InstructionsCanvas.enabled = false;
			effects.SetActive (true);
			backgroundImg.SetActive (true);
			//scrollLeftClick ();
			TransferData.instance.shipID = true;
			TransferData.instance.multiplayerCheck = false;
			splashed = true;
		}

		// Rotate the object around its local X axis at 1 degree per second
		ship.transform.Rotate (Vector3.up * Time.deltaTime * 100);
		car.transform.Rotate (Vector3.up * Time.deltaTime * 100);
	}

	// Start a game singleplayer game
	public void LoadOnSingle()
	{
		SceneManager.LoadScene (4);
	}
    
	// start a multiplayer game
	public void LoadOnMulti()
	{
		TransferData.instance.multiplayerCheck = true;
		SceneManager.LoadScene (5);
	}

	// Open instructions canvas
	public void InstructionsOn()
	{
		SelectionCanvas.enabled = false;
		InstructionsCanvas.enabled = true;
		ship.SetActive (false);
		car.SetActive (false);
	}

	// return to selection
	public void InstructionsOff()
	{
		SelectionCanvas.enabled = true;
		InstructionsCanvas.enabled = false;
		ship.SetActive (true);
		TransferData.instance.shipID = true;
	}

	// handle car rotations
    public void scrollLeftClick() // left selection button selected
    {
		if (car.activeInHierarchy) {
			ship.SetActive (true);
			car.SetActive (false);
			TransferData.instance.shipID = true;
		} else if (ship.activeInHierarchy) {
			ship.SetActive (false);
			car.SetActive (true);
			TransferData.instance.shipID = false;
		} else {
			ship.SetActive (true);
			car.SetActive (false);
		}
    }
    public void scrollRightClick() // right selection button selected
    {
		if (car.activeInHierarchy) {
			ship.SetActive (true);
			car.SetActive (false);
			TransferData.instance.shipID = true;
		}
		else if (ship.activeInHierarchy) {
			ship.SetActive (false);
			car.SetActive (true);
			TransferData.instance.shipID = false;
		}
    }

}
