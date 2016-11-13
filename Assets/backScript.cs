using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class backScript : MonoBehaviour {

	public void LoadOnBack()
	{
		SceneManager.LoadScene (0);
	}
}
