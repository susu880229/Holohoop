using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDownPlayers : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnScaleDown()
    {
        this.transform.localScale -= new Vector3(1f, 1f, 1f);
        Debug.Log(this.name + " " + this.transform.localScale);
    }

    void OnScaleUp()
    {
        this.transform.localScale += new Vector3(1f, 1f, 1f);
        Debug.Log(this.name + " " + this.transform.localScale);
    }
}
