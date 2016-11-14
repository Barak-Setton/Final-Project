using UnityEngine;
using System.Collections;

public class choosePrefab : MonoBehaviour
{
    public GameObject netManager;
    void Start()
    {
        if (TransferData.instance.shipID)
        {
            print("choose = 0 (ship)");
            netManager.GetComponent<GameManagerScript>().btn1();
        }
        else
        {
            print("choose = 1 (car)");
            netManager.GetComponent<GameManagerScript>().btn2();
        }
    }
}
