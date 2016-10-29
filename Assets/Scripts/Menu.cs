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
    private int scrollDirection = 0;
    public int rightTarget = 0;
    public int leftTarget = -100;

	// Boolean to check if game is networked
	private bool isMultiplayer;


    // On Awake Menu Selection is disabled
    void Awake()
	{
        SelectionCanvas.enabled = true;
		ship.SetActive(false);
		car.SetActive(false);

		// car selection controls
        SelectionRenderers = SelectionCanvas.GetComponentsInChildren<Renderer>();
		scrollDirection = 0;
        foreach (Renderer r in SelectionRenderers)
        {
            r.enabled = false;
        }
    }

    // car rotations
    void Update()
	{
		// Rotate the object around its local X axis at 1 degree per second
		ship.transform.Rotate (Vector3.up * Time.deltaTime * 100);
		car.transform.Rotate (Vector3.up * Time.deltaTime * 100);

		// car selection controls
		if (scrollDirection != 0) { // left/right button pressed
			vehicles.transform.Translate (vehicles.transform.right * Time.deltaTime * 200 * scrollDirection); // move the vehicles in direction (1 or -1) 
            
			if (vehicles.transform.position.x > rightTarget) { // ship in center (move all the way right)
				scrollDirection = 0; // stop vehicles moving
				vehicles.transform.position = new Vector3 (rightTarget, 0, 0); // stick vehicles to the far right
			} else if (vehicles.transform.position.x < leftTarget) { // car in the center (moved all the way to the left)
				scrollDirection = 0;// stop vehicles moving
				vehicles.transform.position = new Vector3 (leftTarget, 0, 0);// stick vehicles to the far left
			}
		}
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
        scrollDirection = -1;
    }
    public void scrollRightClick() // right selection button selected
    {
        scrollDirection = 1;
    }

}
