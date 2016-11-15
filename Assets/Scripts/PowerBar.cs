using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour {

    public GameObject player1;

    private PowerbarTracker powerBar;


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if(player1.GetComponent<PowerbarTracker>().hasPower())
        {
			GetComponent<Text>().text =  "Power: "+ (int)player1.GetComponent<PowerbarTracker>().getPower();
        }

    }

    public void setPlayer(GameObject player)
    {
        this.player1 = player;
    }
}
