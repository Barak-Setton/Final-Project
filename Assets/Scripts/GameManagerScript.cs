using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManagerScript : NetworkManager
{

    public int chosenCharacter = 0;

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;
        Debug.Log("server add with message " + selectedClass);
        print(selectedClass);
        if (selectedClass == 0)
        {
            GameObject player = Instantiate(Resources.Load("AirShipCNetwork", typeof(GameObject)) ) as GameObject;
            player.name = "AirShipCNetwork";
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }

        if (selectedClass == 1)
        {
            GameObject player = Instantiate(Resources.Load("groundCarNetwork", typeof(GameObject))) as GameObject;
            player.name = "groundCarNetwork";
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = chosenCharacter;

        ClientScene.AddPlayer(conn, 0, test);
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        //base.OnClientSceneChanged(conn);
    }

    public void btn1()
    {
        print("ship");
        chosenCharacter = 0;
    }

    public void btn2()
    {
        print("Car");
        chosenCharacter = 1;
    }
}
