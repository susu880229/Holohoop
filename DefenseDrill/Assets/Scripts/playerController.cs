using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    GameObject ball;
    ballController ball_script;
    Color32 zone_red;
    Color32 zone_green;
    float stay_time;
    int pre;
    int cur;
    
    // Use this for initialization
    void Start () {

        ball = GameObject.Find("/Basketball Court/halfcourt/ball");
        ball_script = ball.GetComponent<ballController>();
        zone_red = new Color32(255, 0, 0, 178);
        zone_green = new Color32(0, 255, 0, 178);
        pre = -2;
        cur = -1;

        
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("pre" + pre + "cur" + cur);
      
    }

    private void OnTriggerEnter(Collider other)
    {
        //enter the right zone to defense
        
        if (other.gameObject.name == "zone")
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_green;
            
        }

        //enter the right trigger area to defense the opponents' ball

        if (other.gameObject.name == "trigger" + ball_script.to_index)
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_green;
            pre = cur;
            cur = ball_script.to_index;
            if(pre == cur && ball_script.from_index == ball_script.to_index)
            {
                ball_script.pass = false;
            }
            else
            {
                ball_script.pass = true;
            }
        }

    }

    //continuous detecting not one time 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "trigger" + ball_script.to_index)
        {
            
            if(pre != cur)
            {
                ball_script.pass = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.name == "zone")
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_red;
            

        }

        //color feedback when player move out of trigger area
        if (other.gameObject.GetComponent<Renderer>().material.color == zone_green)
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_red;
            ball_script.pass = false;
          
        }
        


    }
}
