using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	//public GUIText countdownText;
	public Image one;
	public Image two;
	public Image three;
	public Image GO;
	private int currentCount = 3;

	public void Awake()
	{
		//countdownText.enabled = true;
		one.enabled = false;
		two.enabled = false;
		three.enabled = false;
		GO.enabled = false;
		StartCoroutine (CountdownFunction ());
	}

	IEnumerator CountdownFunction(){
		for (currentCount = 3; currentCount > -1; currentCount--) {
			if (currentCount == 3) {
				//countdownText.text = currentCount.ToString ();
				one.enabled = false;
				two.enabled = false;
				three.enabled = true;
				GO.enabled = false;
				yield return new WaitForSeconds (1.5f);
			}
			else if (currentCount == 2){
				two.enabled = true;
				one.enabled = false;
				three.enabled = false;
				GO.enabled = false;
				yield return new WaitForSeconds (1.5f);
			}
			else if (currentCount == 1){
				one.enabled = true;
				two.enabled = false;
				three.enabled = false;
				GO.enabled = false;
				yield return new WaitForSeconds (1.5f);
			}
			else {
				//countdownText.text = "GO";
				two.enabled = false;
				one.enabled = false;
				three.enabled = false;
				GO.enabled = true;
				yield return new WaitForSeconds (1.5f);
			}
		}
	}
}
