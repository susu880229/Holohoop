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
	bool bIsPlayerInTrigger;
    Renderer TriggerRenderer;
    public Canvas TimerUI;

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
		player0 = GameObject.Find("/Basketball Court/halfcourt/player0/player0mat");
		player1 = GameObject.Find("/Basketball Court/halfcourt/player1/player1mat");
		player2 = GameObject.Find("/Basketball Court/halfcourt/player2/player2mat");
        TriggerPointer = GameObject.Find("/Basketball Court/TriggerPointer/default");   
        TriggerRenderer = TriggerPointer.GetComponent<Renderer>();
		bIsPlayerInTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("pre" + pre + "cur" + cur);

    }

    IEnumerator OnTriggerEnter(Collider other)
    {
       

        //enter the right trigger area to defense the opponents' ball

        if (other.gameObject.name == "trigger" + ball_script.to_index)
        {
			bIsPlayerInTrigger = true;
			//changing the trigger to green
            //other.gameObject.GetComponent<Renderer>().material.color = zone_green;
			//changign the shirt of the attacker from grey --> green;
			if (ball_script.to_index == 0) {
				player0.gameObject.GetComponent<Renderer> ().material.color = zone_green;
			} else if (ball_script.to_index == 1) {
				player1.gameObject.GetComponent<Renderer> ().material.color = zone_green;
                if (!ball_script.StartPlay)
                {
                    TimerUI.GetComponent<CanvasGroup>().alpha = 1f;
                    TriggerRenderer.enabled = false;
                }
			} else if (ball_script.to_index == 2) {
				player2.gameObject.GetComponent<Renderer> ().material.color = zone_green;
			}

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
    }

	public bool getIsPlayerInTrigger(){
		return bIsPlayerInTrigger;
	}
}
