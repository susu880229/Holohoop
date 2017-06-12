using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class cameraAtGoal : MonoBehaviour {
	public GameObject passingSpot;
	public Text winText;
	// Use this for initialization
	void Start () {
		winText.text = "FAILED!";
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(Camera.main.transform.position, passingSpot.transform.position) < 1.9f){
			//winText.enabled = true;
			winText.text = "SUCCESS!";
		}

		if(Vector3.Distance(Camera.main.transform.position, passingSpot.transform.position) > 1.9f){
			winText.text = "Move to the red circle";
			//winText.enabled = false;
		}
	}
}
