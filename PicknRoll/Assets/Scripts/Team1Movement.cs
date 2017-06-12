using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team1Movement : MonoBehaviour {
	float totalZMove = 0;
	float totalXMove = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float xMove = 0.008f;
		float zMove = 0.008f;

		if(totalXMove < 1){
			transform.Translate(xMove, 0, 0);
			totalXMove += xMove;
		}

		if(totalZMove < 5){
			transform.Translate(0, 0, zMove);
			totalZMove += zMove;
		}
	}
}
