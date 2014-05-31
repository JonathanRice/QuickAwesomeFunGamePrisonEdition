using UnityEngine;
using System.Collections;

public class MainMenuRun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)) {
			loadNextLevel();
		}
	}
	void loadNextLevel() {
		//TODO some kind of transition
		Application.LoadLevel("StealThePie");
	}
}
