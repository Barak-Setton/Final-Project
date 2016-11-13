using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PowerBarNetwork : NetworkBehaviour
{

    public GameObject player1;

    private PowerbarTracker powerBar;


    // Update is called once per frame
    void LateUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (player1.GetComponent<PowerbarTracker>().hasPower())
        {
            GetComponent<Text>().text = "Power: " + player1.GetComponent<PowerbarTracker>().getPower();
        }

    }

    public void setPlayer(GameObject player)
    {
        this.player1 = player;
    }
}
