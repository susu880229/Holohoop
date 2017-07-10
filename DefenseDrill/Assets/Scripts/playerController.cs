using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    GameObject ball;
    ballController ball_script;
    Color32 zone_red;
    Color32 zone_green;
    Color32 zone_grey;
    Color32 zone_blue;
    float stay_time;
    int pre;
    int cur;
    GameObject player0;
    GameObject player1;
    GameObject player2;

    // Use this for initialization
    public void Start()
    {

        ball = GameObject.Find("/Basketball Court/halfcourt/ball");
        ball_script = ball.GetComponent<ballController>();
        zone_red = new Color32(255, 0, 0, 255);
        zone_green = new Color32(0, 255, 0, 178);
        zone_grey = new Color32(128, 128, 128, 255);
        zone_blue = new Color32(75, 139, 148, 96);
        pre = -2;
        cur = -1;
        player0 = GameObject.Find("player0mat");
        player1 = GameObject.Find("player1mat");
        player2 = GameObject.Find("player2mat");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("pre" + pre + "cur" + cur);

    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        //enter the right zone to defense

        if (other.gameObject.name == "zone")
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_green;
            if (other.gameObject.name == "trigger0")
            {
                player0.GetComponent<Renderer>().material.color = zone_green;
            }
            if (other.gameObject.name == "trigger1")
            {
                player1.GetComponent<Renderer>().material.color = zone_green;
            }
            if (other.gameObject.name == "trigger2")
            {
                player2.GetComponent<Renderer>().material.color = zone_green;
            }

        }

        //enter the right trigger area to defense the opponents' ball

        if (other.gameObject.name == "trigger" + ball_script.to_index)
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_green;
            if (!ball_script.StartPlay)
            {
                //yield until user start to play
                yield return new WaitUntil(() => ball_script.StartPlay == true);

            }
            pre = cur;
            cur = ball_script.to_index;
            if (pre == cur && ball_script.from_index == ball_script.to_index)
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

            if (pre != cur)
            {
                ball_script.pass = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "zone")
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_blue;
            if (other.gameObject.name == "trigger0")
            {
                player0.GetComponent<Renderer>().material.color = zone_grey;
            }
            if (other.gameObject.name == "trigger1")
            {
                player1.GetComponent<Renderer>().material.color = zone_grey;
            }
            if (other.gameObject.name == "trigger2")
            {
                player2.GetComponent<Renderer>().material.color = zone_grey;
            }

        }

        //color feedback when player move out of trigger area
        if (other.gameObject.GetComponent<Renderer>().material.color == zone_green)
        {

            other.gameObject.GetComponent<Renderer>().material.color = zone_blue;
            ball_script.pass = false;
            if (other.gameObject.name == "trigger0")
            {
                player0.GetComponent<Renderer>().material.color = zone_grey;
            }
            if (other.gameObject.name == "trigger1")
            {
                player1.GetComponent<Renderer>().material.color = zone_grey;
            }
            if (other.gameObject.name == "trigger2")
            {
                player2.GetComponent<Renderer>().material.color = zone_grey;
            }

        }



    }
}
