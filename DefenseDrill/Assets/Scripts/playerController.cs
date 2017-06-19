using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    GameObject ball;
    ballController ball_script;
    Color32 zone_red;
    Color32 zone_green;
    public bool enter;
    


    // Use this for initialization
    void Start () {

        ball = GameObject.Find("/Basketball Court/halfcourt/ball");
        ball_script = ball.GetComponent<ballController>();
        zone_red = new Color32(255, 0, 0, 178);
        zone_green = new Color32(0, 255, 0, 178);
        enter = false;


    }
	
	// Update is called once per frame
	void Update () {

        
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //enter the right zone to defense
        
        if (other.gameObject.name == "zone")
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_green;

            enter = true;
            
        }
        
        //enter the right trigger area to defense the opponents' ball
        if (ball_script.count_down)
        {
           
            if (other.gameObject.name == "trigger" + ball_script.to_index)
            {
                
                other.gameObject.GetComponent<Renderer>().material.color = zone_green;

                
                ball_script.pass = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.name == "zone")
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_red;
            enter = false;

        }

        if (other.gameObject.GetComponent<Renderer>().material.color == zone_green)
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_red;

        }


    }
}
