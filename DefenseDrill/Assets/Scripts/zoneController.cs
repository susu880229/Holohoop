using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoneController : MonoBehaviour {

    Color32 zone_red;
    Color32 zone_green;
    public bool enter;

	// Use this for initialization
	void Start () {

        zone_red = new Color32(255, 0, 0, 178);
        zone_green = new Color32(0, 255, 0, 178);
        enter = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Main Camera")
        {

            GetComponent<Renderer>().material.color = zone_green;
            enter = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Main Camera")
        {

            GetComponent<Renderer>().material.color = zone_red;
            enter = false;
        }
        
    }
}
