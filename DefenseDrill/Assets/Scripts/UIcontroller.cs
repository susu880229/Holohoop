using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontroller : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

        
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram...
            
            transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - 3f);

            // Rotate the cursor to hug the surface of the hologram.
            Vector3 newRot = new Vector3(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Euler(newRot);

        }
        
        


    }
}
