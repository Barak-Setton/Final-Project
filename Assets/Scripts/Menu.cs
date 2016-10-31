using UnityEngine;
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
    public RectTransform vehicles;
	Renderer[] SelectionRenderers;
    public int rightTarget = 0;
    public int leftTarget = -100;

	// Boolean to check if game is networked
	public bool isMultiplayer;
	public bool isShip;

    // On Awake Menu Selection is disabled
    void Awake()
	{
        SelectionCanvas.enabled = true;

		// car selection controls
        SelectionRenderers = SelectionCanvas.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in SelectionRenderers)
        {
            r.enabled = false;
        }
    }

	void Start()
	{
		ship.SetActive(false);
		car.SetActive(false);
		scrollLeftClick ();
		isShip = true;
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
		SceneManager.LoadScene (1);
	}
    
	// start a multiplayer game
	public void LoadOnMulti()
	{
		isMultiplayer = true;
		SceneManager.LoadScene (3);
	}

	// handle car rotations
    public void scrollLeftClick() // left selection button selected
    {
		if (car.activeInHierarchy) {
			ship.SetActive (true);
			car.SetActive (false);
			isShip = true;
		} else if (ship.activeInHierarchy) {
			ship.SetActive (false);
			car.SetActive (true);
			isShip = false;
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
			isShip = true;
		}
		else if (ship.activeInHierarchy) {
			ship.SetActive (false);
			car.SetActive (true);
			isShip = false;
		}
    }

}
