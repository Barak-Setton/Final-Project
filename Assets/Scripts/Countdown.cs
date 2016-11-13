using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	//public GUIText countdownText;
	public SpriteRenderer one;
	public SpriteRenderer two;
	public SpriteRenderer three;
	public SpriteRenderer GO;
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
				yield return new WaitForSeconds (1.5f);
			}
			else if (currentCount == 2){
				
			}
			else if (currentCount == 1){

			}
			else {
				//countdownText.text = "GO";
				yield return new WaitForSeconds (1.5f);
			}
		}
		//countdownText.enabled = false;
	}
}
