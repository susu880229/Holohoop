using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

	private Transform Cam;

	// Use this for initialization
	void Start () {
		Cam = Camera.main.transform;

	}

	// Update is called once per frame
	void Update () {
		//Vector3 BallPos = new Vector3 (Cam.transform.position.x + 0.2f, 1f, Cam.transform.position.z + 1f) + new Vector3(0f, 0.6f * Mathf.Sin(5f * Time.time), 0f);
		Vector3 BallPos = new Vector3(this.transform.position.x, 1f, this.transform.position.z);
		this.transform.position = BallPos + new Vector3(0f, 0.6f * Mathf.Sin(5f * Time.time), 0f);
	}
}
