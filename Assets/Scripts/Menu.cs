using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

public class Menu : NetworkBehaviour 
{

    public GameObject server;
    NetworkManagerHUD hudManager;

    // Canvas objects
    public Canvas ModeCanvas;
	public Canvas SelectionCanvas;
    public Canvas LobbyCanvas; 
	public Image SplashScreen;

    // vehicel objetcs
    public GameObject ship;
    public GameObject car;
    public RectTransform vehicles;

    private int scrollDirection = 0;
    public int rightTarget = 0;
    public int leftTarget = -100;

	// Boolean to check if game is networked
	private bool isMultiplayer;
	// counter to track game time
	private float counter = 0;

    Renderer[] SelectionRenderers;


    // On Awake Menu Selection is disabled
    void Awake()
	{
        scrollDirection = 0;

        LobbyCanvas.enabled = false;
        hudManager = server.GetComponent<NetworkManagerHUD>();
        hudManager.showGUI = false;
    
        SelectionCanvas.enabled = false;
        SelectionRenderers = SelectionCanvas.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in SelectionRenderers)
        {
            r.enabled = false;
        }
    }

    // update counter and handle splash screen
    void Update()
	{
        // Rotate the object around its local X axis at 1 degree per second
        ship.transform.Rotate(Vector3.up * Time.deltaTime*100);
        car.transform.Rotate(Vector3.up * Time.deltaTime*100);

        if (scrollDirection !=0) // left/right button pressed
        {
            vehicles.transform.Translate(vehicles.transform.right * Time.deltaTime * 200 * scrollDirection); // move the vehicles in direction (1 or -1) 
            
            if (vehicles.transform.position.x > rightTarget) // ship in center (move all the way right)
            {
                scrollDirection = 0; // stop vehicles moving
                vehicles.transform.position = new Vector3(rightTarget, 0, 0); // stick vehicles to the far right
            }
            else if(vehicles.transform.position.x < leftTarget) // car in the center (moved all the way to the left)
            {
                scrollDirection = 0;// stop vehicles moving
                vehicles.transform.position = new Vector3(leftTarget, 0, 0);// stick vehicles to the far left
            }
        }

        counter += Time.deltaTime;
		if (counter >= 2) 
		{
			SplashScreen.enabled = false;
		}
	}

	// Select Multiplayer Mode
	public void LobbySelection()
	{
        LobbyCanvas.enabled = true;
        hudManager.showGUI = true;
        SelectionCanvas.enabled = false;
        foreach (Renderer r in SelectionRenderers)
        {
            r.enabled = false;
        }
        ModeCanvas.enabled = false;
		isMultiplayer = true;
	}

    public void MultiplayerSelection()
    {
        LobbyCanvas.enabled = false;
        hudManager.showGUI = false;
        SelectionCanvas.enabled = true;
        foreach (Renderer r in SelectionRenderers)
        {
            r.enabled = true;
        }
    }

	// Select Solo Mode
	public void SoloSelectionOn()
	{
		SelectionCanvas.enabled = true;
        // rendering all children of this gameobject to show the vehicles
        foreach (Renderer r in SelectionRenderers)
        {
            r.enabled = true;
        }
        ModeCanvas.enabled = false;
		isMultiplayer = false;
	}

	// Start a game, determine if mulitplayer or solo
	public void LoadOn()
	{

		if (isMultiplayer) 
		{
            SceneManager.LoadScene (3);
        }
		else if (!isMultiplayer) 
		{
			SceneManager.LoadScene (1);
		}
	}
    
    public void scrollLeftClick() // left selection button selected
    {
        scrollDirection = -1;
    }
    public void scrollRightClick() // right selection button selected
    {
        scrollDirection = 1;
    }

}
