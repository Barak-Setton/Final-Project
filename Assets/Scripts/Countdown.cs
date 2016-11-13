using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	public GUIText countdownText;
	private int currentCount = 3;

	public void Start()
	{
		countdownText.enabled = true;
		StartCoroutine (CountdownFunction ());
	}

	IEnumerator CountdownFunction(){
		for (currentCount = 3; currentCount > -1; currentCount--) {
			if (currentCount != 0) {
				countdownText.text = currentCount.ToString ();
				yield return new WaitForSeconds (1.5f);
			}
			else if (currentCount == 2){
				
			}
			else if (currentCount == 1){

			}
			else {
				countdownText.text = "GO";
				yield return new WaitForSeconds (1.5f);
			}
		}
		countdownText.enabled = false;
	}
}
