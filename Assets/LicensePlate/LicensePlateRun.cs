using UnityEngine;
using System.Collections;

public class LicensePlateRun : MonoBehaviour {
	public GameObject licensePressDown;
	public GameObject licensePressStamped;
	public GameObject licensePressUpPlateDown;
	public GameObject licensePressUpPlateEmpty;
	public GameObject licensePressUpPlateUp;
	public GameObject licensePressUpPlateFail;
	bool acceptInput = true;
	// Use this for initialization
	void Start () {

		this.setAllRenderFalse();
		licensePressUpPlateUp.renderer.enabled = true;
		StartCoroutine("countDownToLoose");
	}
	
	// Update is called once per frame
	void Update () {
		if(acceptInput && Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click.");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
			Vector3 point = ray.origin + (ray.direction * 10);    
			Debug.Log( "World point " + point );
			if (point.x > 0) {
				Debug.Log ("Red button pushed");
				StartCoroutine("licensePress");
				//TODO we need to eventually throw a new plate at the user

			} else {
				Debug.Log ("License plate pushed");

				if (licensePressUpPlateDown.renderer.enabled) {
					setAllRenderFalse();
					licensePressUpPlateUp.renderer.enabled = true;

				} else {
					setAllRenderFalse();
					licensePressUpPlateDown.renderer.enabled = true;
				}

			}
		}
	}

	void setAllRenderFalse() {
		licensePressDown.renderer.enabled = false;
		licensePressStamped.renderer.enabled = false;
		licensePressUpPlateDown.renderer.enabled = false;
		licensePressUpPlateEmpty.renderer.enabled = false;
		licensePressUpPlateUp.renderer.enabled = false;
		licensePressUpPlateFail.renderer.enabled = false;
	}

	IEnumerator licensePress() {
		//Turn off the input
		acceptInput = false;
		// only render the license press down for a second
		bool playerWin = false;
		if (licensePressUpPlateUp.renderer.enabled == true) {
			playerWin = true;
		}
		setAllRenderFalse();
		licensePressDown.renderer.enabled = true;
		yield return new WaitForSeconds(1.3f);

		if (playerWin) {
			// only render the final license plat process for a second
			licensePressDown.renderer.enabled = false;
			licensePressStamped.renderer.enabled = true;
			yield return new WaitForSeconds(1f);
		} else {
			licensePressDown.renderer.enabled = false;
			licensePressUpPlateFail.renderer.enabled = true;
			yield return new WaitForSeconds(1f);
		}
		// only render the blank press for a second
		licensePressStamped.renderer.enabled = false;
		licensePressUpPlateEmpty.renderer.enabled = true;
		yield return new WaitForSeconds(0.5f);
		// now render a random state
		licensePressUpPlateEmpty.renderer.enabled = false;
		float rand = Random.Range(-1f, 1f);
		if (rand > 0) {
			licensePressUpPlateDown.renderer.enabled = true;
		} else {
			licensePressUpPlateUp.renderer.enabled = true;
		}

		acceptInput = true;

	}

	IEnumerator countDownToLoose() {
		yield return new WaitForSeconds(10.0f);
		//TODO some kind of transition
		//Time is up
		Application.LoadLevel("ClimbTheWall");
	}
}
