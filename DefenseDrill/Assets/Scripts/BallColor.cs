using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColor : MonoBehaviour {

    public Material BallOrange;
    public Material BallBlue;
    bool isOrange;

	// Use this for initialization
	void Start () {
        isOrange = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnBallColor()
    {
        if (isOrange)
        {
            this.GetComponent<Renderer>().material = BallBlue;
            isOrange = false;
        }
        else
        {
            this.GetComponent<Renderer>().material = BallOrange;
            isOrange = true;
        }
        
    }

}
