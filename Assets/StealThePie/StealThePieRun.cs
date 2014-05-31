using UnityEngine;
using System.Collections;

public class StealThePieRun : MonoBehaviour {
	public GameObject background;
	public GameObject woman;
	public GameObject theif;
	public GameObject pie;
	public GameObject win;
	public GameObject fail;
	public GameObject transition;

	bool isWin;
	bool acceptInput;
	// Use this for initialization
	void Start () {
		win.renderer.enabled = false;
		fail.renderer.enabled = false;
		transition.renderer.enabled = false;
		acceptInput = true;
		isWin = false;
		StartCoroutine("countDownToLoose");
		StartCoroutine("womanMove");
	}
	
	// Update is called once per frame
	void Update () {
		//Dont run any of this if we have already lost and are just failing the guy
		if (acceptInput) {
			if(Input.GetMouseButtonUp(0)) {
				if (woman.transform.localScale.x < 0) { // The woman is looking away so we steal and win
					//Lets do the pie stealing bit
					theif.transform.localScale = new Vector3(-1,1,1);
					pie.transform.position = new Vector3(0.4014753f, -1.026456f, 0);
					StartCoroutine("winNow");
				} else { // the woman is looking at us so we fail
					StartCoroutine ("failNow");
				}
			}
		}
	}

	IEnumerator failNow() {
		isWin = false;
		fail.renderer.enabled = true;
		yield return new WaitForSeconds(1.0f);
		StartCoroutine(loadNextLevel());
	}
	IEnumerator winNow() {
		isWin = true;
		yield return new WaitForSeconds(1.0f);
		win.renderer.enabled = true;
		yield return new WaitForSeconds(1.0f);
		StartCoroutine(loadNextLevel());
	}
	// This will make the sure woman flips back and forth three times in the 9 second time limit, but at slightly random intervals
	IEnumerator womanMove() {
		float waitTime = Random.Range(0.3f, 1.0f);
		//We are going to look away somewhere between .3 and 1 second into the game
		yield return new WaitForSeconds(waitTime);
		woman.transform.localScale = new Vector3(-1, 1, 1);
		yield return new WaitForSeconds(0.5f);
		//The woman has now looked away and waited .5 seconds
		woman.transform.localScale = new Vector3(1, 1, 1);
		// the woman is now looking back
		// We will now wait until 3 seconds has passed
		yield return new WaitForSeconds(3.0f - 0.5f - waitTime);

		// We are now at 3 seconds in
		//This time around wait randomly between 1 and 3 seconds.  This means the second turn will happen between 4 and 6 seconds
		waitTime = Random.Range(1.0f, 2.0f);
		yield return new WaitForSeconds(waitTime);
		woman.transform.localScale = new Vector3(-1, 1, 1);
		yield return new WaitForSeconds(1.0f);
		//The woman has now looked away and waited 1.5 seconds
		woman.transform.localScale = new Vector3(1, 1, 1);

		// Do it again
		waitTime = Random.Range(1.0f, 2.0f);
		yield return new WaitForSeconds(waitTime);
		woman.transform.localScale = new Vector3(-1, 1, 1);
		yield return new WaitForSeconds(1.0f);
		//The woman has now looked away and waited 1.5 seconds
		woman.transform.localScale = new Vector3(1, 1, 1);

	}
	IEnumerator countDownToLoose() {
		yield return new WaitForSeconds(9.0f);
		if (!isWin) {
			acceptInput = false;
			fail.renderer.enabled = true;
			yield return new WaitForSeconds(1.0f);
			StartCoroutine(loadNextLevel());
		}
	}
	IEnumerator loadNextLevel() {
		transition.renderer.enabled = true;
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel("LicensePlate");
	}
}
