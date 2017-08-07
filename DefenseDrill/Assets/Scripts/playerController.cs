using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    GameObject ball;
    ballController ball_script;
    Color32 zone_red;
    Color32 zone_green;
    Color32 zone_grey;
    Color32 zone_blue;
    int pre;
    int cur;
    GameObject player0;
    GameObject player1;
    GameObject player2;
    GameObject TriggerPointer;
	public bool bIsPlayerInTrigger;
	bool bExitWalkThru;
    Renderer TriggerRenderer;
    public Canvas TimerUI;
	public AudioSource[] walkThruClips;

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
		player0 = GameObject.Find("/Basketball Court/halfcourt/player0/polySurface17");
		player1 = GameObject.Find("/Basketball Court/halfcourt/player1/polySurface17");
		player2 = GameObject.Find("/Basketball Court/halfcourt/player2/polySurface17");
        TriggerPointer = GameObject.Find("/Basketball Court/TriggerPointer/default");   
        TriggerRenderer = TriggerPointer.GetComponent<Renderer>();
		bIsPlayerInTrigger = false;
		bExitWalkThru = false;
		walkThruClips = GetComponents<AudioSource> ();
		playWalkthrough();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("pre" + pre + "cur" + cur);
    }

   
	public void playWalkthrough(){
			Debug.Log ("Play walk through");
			//audio clip contains Intro walkthrough (10 secs to look around) + 15s blank + Repeat msg + 15s blank
			walkThruClips[0].Play();
	}

	private void OnTriggerEnter(Collider other)
    {
       

        //enter the right trigger area to defense the opponents' ball

        if (other.gameObject.name == "trigger" + ball_script.to_index)
        {
			
            //bIsPlayerInTrigger = true;
            //other.gameObject.GetComponent<Renderer>().material.color = zone_green;
            //changign the shirt of the attacker from grey --> green;
            player_Green();
            pre = cur;
            cur = ball_script.to_index;
            //imporve pass successful rate (shoot only when the ball is already on the center of trigger area)
            if(pre == cur && ball.transform.position == ball_script.opp_positions[ball_script.to_index])
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
            //to avoid sometimes it didn't turn green after restart and player stand in the middle
            player_Green();

            if (pre != cur)
            {
                ball_script.pass = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        
		bIsPlayerInTrigger = false;
		//when player move out of trigger area, change color back to grey
		if (other.gameObject.name == "trigger0")
		{
			player0.GetComponent<Renderer>().material.color = zone_grey;
            ball_script.pass = false;
        }
		if (other.gameObject.name == "trigger1")
		{
            if (!ball_script.StartPlay)
            {
                //TimerUI.GetComponent<Text>().text = "";
                TimerUI.GetComponent<CanvasGroup>().alpha = 0f;
                TriggerRenderer.enabled = true;
                Reset(); //reset the pre and cur when staryPlay = false
            }
            player1.GetComponent<Renderer>().material.color = zone_grey;
            ball_script.pass = false;
        }
		if (other.gameObject.name == "trigger2")
		{
			player2.GetComponent<Renderer>().material.color = zone_grey;
            ball_script.pass = false;
        }



    }

    public void Reset()
    {
        pre = -2;
        cur = -1;
        TriggerRenderer.enabled = true; //the triggerPointer still there after restart
    }

	public bool getIsPlayerInTrigger()
    {
		return bIsPlayerInTrigger;
	}

	public bool getExitWalkThru(){
		return bExitWalkThru;
	}
	public void setExitWalkThru(bool extiWalkThru){
		bExitWalkThru = extiWalkThru;
	}

    //change the clothes mesh part material to change color green
    void player_Green()
    {
        if (ball_script.to_index == 0)
        {
            player0.gameObject.GetComponent<Renderer>().material.color = zone_green;
        }
        else if (ball_script.to_index == 1)
        {
            player1.gameObject.GetComponent<Renderer>().material.color = zone_green;
            bIsPlayerInTrigger = true;
            if (!ball_script.StartPlay)
                {
                    TimerUI.GetComponent<CanvasGroup>().alpha = 1f;
                    if(!ball_script.start_count)
                    {
                      TimerUI.transform.Find("Start_Timer").GetComponent<Text>().text = "start!";
                    }
                
                    TriggerRenderer.enabled = false;
                }
        }
        else if (ball_script.to_index == 2)
        {
            player2.gameObject.GetComponent<Renderer>().material.color = zone_green;
        }
    }
}
