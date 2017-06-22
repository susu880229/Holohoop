using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceRestart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnRestart()
    {
        GameObject b = GameObject.Find("/Basketball Court/halfcourt/ball");
        GameObject p = GameObject.Find("/Main Camera");
        Debug.Log("Restart");
        b.GetComponent<ballController>().Start();
        p.GetComponent<playerController>().Start();
    }
}
