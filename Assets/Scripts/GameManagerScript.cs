﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManagerScript : NetworkManager
{

    NetworkMessage test = new NetworkMessage();

    public int chosenCharacter;
    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }

    public void Start()
    {
        ClientScene.RegisterPrefab(Resources.Load("AirShipNetwork(camera)") as GameObject);
        ClientScene.RegisterPrefab(Resources.Load("groundCarNetwork(camera)") as GameObject);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;

        Debug.Log("server add with message " + selectedClass);

        if (selectedClass == 0)
        {
            GameObject player = Instantiate(Resources.Load("AirShipNetwork(camera)", typeof(GameObject)), new Vector3(0, 0, 0), new Quaternion(0,0,0,0)) as GameObject;
            player.name = "AirshipNetwork(camera)";
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            //ClientScene.RegisterPrefab(player);
        }

        if (selectedClass == 1)
        {
            GameObject player = Instantiate(Resources.Load("groundCarNetwork(camera)", typeof(GameObject)), new Vector3(0,0,0), new Quaternion(0, 0, 0, 0)) as GameObject;
            player.name = "groundCarNetwork(camera)";
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
           // ClientScene.RegisterPrefab(player);

        }
        print("before/after");
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        test.chosenClass = chosenCharacter;
        ClientScene.AddPlayer(conn, 0, test);
    }


    public void btn1()
    {
        chosenCharacter = 0;
    }

    public void btn2()
    {
        chosenCharacter = 1;
    }
}
