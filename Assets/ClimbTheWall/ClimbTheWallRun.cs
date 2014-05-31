using UnityEngine;
using System.Collections;

public class ClimbTheWallRun : MonoBehaviour {
	public GameObject background;
	public GameObject backgroundFree;
	public GameObject man;
	public GameObject transition;
	public GameObject fail;

	// Use this for initialization
	void Start () {
		backgroundFree.renderer.enabled = false;
		transition.renderer.enabled = false;
		fail.renderer.enabled = false;
		StartCoroutine("countDownToLoose");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)) {
			Debug.Log("Pressed left click.");
			man.transform.position = new Vector3(man.transform.position.x, man.transform.position.y + 0.16f, man.transform.position.z); 
		}
		if (man.transform.position.y > 2) {
			//They made it over the wall, win.
			StartCoroutine("overTheWall");
		}
	}

	IEnumerator overTheWall() {
		backgroundFree.renderer.enabled = true;
		background.renderer.enabled = false;
		yield return new WaitForSeconds(1.0f);
		StartCoroutine(loadNextLevel());
	}

	IEnumerator countDownToLoose() {
		yield return new WaitForSeconds(9.0f);
		if (backgroundFree.renderer.enabled == false) {
			fail.renderer.enabled = true;
			yield return new WaitForSeconds(1.0f);
			StartCoroutine(loadNextLevel());
		}
	}

	IEnumerator loadNextLevel() {
		transition.renderer.enabled = true;
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel("StealThePie");
	}
}
