using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
public class PowerBar : NetworkBehaviour {

	public GameObject player1;

	private NetworkPowerbarTracker powerBar;

	void LateUpdate () {
		if (!isLocalPlayer)
			return;
		if(player1.GetComponent<NetworkPowerbarTracker>().hasPower())
		{
			GetComponent<Text>().text =  "Power: "+ player1.GetComponent<NetworkPowerbarTracker>().getPower();
		}

	}

	public void setPlayer(GameObject player)
	{
		this.player1 = player;
	}
}

