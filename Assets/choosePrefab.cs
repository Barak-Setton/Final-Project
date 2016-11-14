using UnityEngine;
using System.Collections;

public class choosePrefab : MonoBehaviour
{
    public GameObject netManager;
    void Start()
    {
        if (TransferData.instance.shipID)
        {
            netManager.GetComponent<GameManagerScript>().btn1();
        }
        else
        {
            netManager.GetComponent<GameManagerScript>().btn2();
        }
    }
}
