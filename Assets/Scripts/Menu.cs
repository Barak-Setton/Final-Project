﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

public class Menu : NetworkBehaviour 
{
    // Canvas objects
	public Canvas SelectionCanvas;

    // vehicel objetcs
    public GameObject ship;
    public GameObject car;
    public int rightTarget = 0;
    public int leftTarget = -100;

    // On Awake Menu Selection is disabled
    void Awake()
	{
        SelectionCanvas.enabled = true;
    }

	void Start()
	{
		ship.SetActive(false);
		car.SetActive(false);
		scrollLeftClick ();
		TransferData.instance.shipID = true;
	}

    // car rotations
    void Update()
	{
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
