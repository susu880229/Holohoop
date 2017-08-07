using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustHeight : MonoBehaviour {

    GameObject ball;
    ballController ball_script;
	// Use this for initialization
	void Start () {
        ball = GameObject.Find("/Basketball Court/halfcourt/ball");
        ball_script = ball.GetComponent<ballController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnHigherUp()
    {
        this.transform.position += new Vector3(0f, 0.3f, 0f);
        ball_script.SetOppPositions();
        Debug.Log(this.name + " " + this.transform.position);
    }

    void OnLowerDown()
    {
        this.transform.position -= new Vector3(0f, 0.3f, 0f);
        ball_script.SetOppPositions();
        Debug.Log(this.name + " " + this.transform.position);
    }
}
