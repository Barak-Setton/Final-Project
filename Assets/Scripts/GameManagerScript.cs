using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManagerScript : NetworkManager
{

    NetworkMessage test = new NetworkMessage();

    public int chosenCharacter;
    public short playerID = -1;
    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
        public short playerid;
    }

    public void Start()
    {

        ClientScene.RegisterPrefab(Resources.Load("AirShipNetwork(camera)") as GameObject);
        ClientScene.RegisterPrefab(Resources.Load("groundCarNetwork(camera)") as GameObject);

        if (chosenCharacter == 0)
        {
            
        }

        if (chosenCharacter == 1)
        {
            
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;

        Debug.Log("server add with message " + selectedClass);

        if (selectedClass == 0)
        {
            GameObject player = Instantiate(Resources.Load("AirShipNetwork(camera)", typeof(GameObject)), new Vector3(342, 9, -7), new Quaternion(0,0,0,0)) as GameObject;
            player.name = "AirShipCNetwork(camera)";
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            //ClientScene.RegisterPrefab(player);
        }

        if (selectedClass == 1)
        {
            GameObject player = Instantiate(Resources.Load("groundCarNetwork(camera)", typeof(GameObject)), new Vector3(342, 9, -7), new Quaternion(0, 0, 0, 0)) as GameObject;
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
        print("before");
        ClientScene.AddPlayer(conn, playerID, test);

        print("after");
        print(networkAddress + " " + networkPort);
        

    }


    public void btn1()
    {
        chosenCharacter = 0;
        playerID++;
        print(playerID);
    }

    public void btn2()
    {
        chosenCharacter = 1;
        playerID++;
        print(playerID);
    }
}
