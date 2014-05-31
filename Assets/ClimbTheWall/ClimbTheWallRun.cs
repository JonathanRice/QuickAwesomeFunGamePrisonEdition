using UnityEngine;
using System.Collections;

public class ClimbTheWallRun : MonoBehaviour {
	public GameObject background;
	public GameObject backgroundFree;
	public GameObject man;
	// Use this for initialization
	void Start () {
		backgroundFree.renderer.enabled = false;
		StartCoroutine("countDownToLoose");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)) {
			Debug.Log("Pressed left click.");
			man.transform.position = new Vector3(man.transform.position.x, man.transform.position.y + 0.1f, man.transform.position.z); 
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
		loadNextLevel();
	}

	IEnumerator countDownToLoose() {
		yield return new WaitForSeconds(10.0f);
		if (backgroundFree.renderer.enabled == false) {
			//TODO some kind of transition
			//Time is up and we are not headed toward the win screen
			loadNextLevel();
		}
	}

	void loadNextLevel() {
		//TODO some kind of transition
		Application.LoadLevel("StealThePie");
	}
}
