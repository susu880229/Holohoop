using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustHeight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnHigherUp()
    {
        this.transform.position += new Vector3(0f, 0.3f, 0f);
        Debug.Log(this.name + " " + this.transform.position);
    }

    void OnLowerDown()
    {
        this.transform.position -= new Vector3(0f, 0.3f, 0f);
        Debug.Log(this.name + " " + this.transform.position);
    }
}
