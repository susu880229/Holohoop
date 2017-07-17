using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballController : MonoBehaviour
{

    Vector3[] opp_positions;
    GameObject opp0;
    GameObject opp1;
    GameObject opp2;
    GameObject player0;
    GameObject player1;
    GameObject player2;
    GameObject zone;
    public GameObject rim;
    GameObject player;
    GameObject start_timerUI;
    GameObject result_UI;
    playerController player_script;
    Vector3 from;
    public int from_index;
    public Vector3 to;
    public int to_index;
    public float move_speed = 1f;
    public float delay_time = 3f;
    public float shoot_speed = 6f;
    public float pass_speed = 1f;
    public bool pass;

    //start timer varibles
    public float startTimer = 4f;
    bool start_count;

    //successful drill varibles
    public float success_time = 60f;
    float play_time;
    bool play_count;


    public bool StartPlay;
    public bool PausePlay;
    public bool RestartPlay;
    CanvasGroup start_canvas;
    CanvasGroup result_canvas;
    private Animator[] Anim;
	AudioSource[] allSounds;
	bool bCheckResultRun; 


    // Use this for initialization
    public void Start()
    {
        
        StartPlay = false;
        PausePlay = false;
        RestartPlay = false;
        start_count = false;

        play_time = 0f;
        play_count = true;

        pass = false;

        opp0 = GameObject.Find("/Basketball Court/halfcourt/opp0");
        opp1 = GameObject.Find("/Basketball Court/halfcourt/opp1");
        opp2 = GameObject.Find("/Basketball Court/halfcourt/opp2");

        player0 = GameObject.Find("/Basketball Court/halfcourt/player0");
        player1 = GameObject.Find("/Basketball Court/halfcourt/player1");
        player2 = GameObject.Find("/Basketball Court/halfcourt/player2");
        player = GameObject.Find("/Main Camera");

        player_script = player.GetComponent<playerController>();

        start_timerUI = GameObject.Find("Main Camera/Timer_UI/Start_Timer");
        result_UI = GameObject.Find("Main Camera/Result_UI/Result");
        //start_timerUI.GetComponent<Text>().text = startTimer.ToString();
        
        start_timerUI.GetComponent<Text>().text = "start!";
        start_canvas = GameObject.Find("Main Camera/Timer_UI").GetComponent<CanvasGroup>();
        result_canvas = GameObject.Find("Main Camera/Result_UI").GetComponent<CanvasGroup>();
        //visi_canvas(start_canvas);
        invi_canvas(result_canvas);

        Anim = new Animator[3];
        Anim[0] = player0.GetComponent<Animator>();
        Anim[1] = player1.GetComponent<Animator>();
        Anim[2] = player2.GetComponent<Animator>();

        zone = GameObject.Find("/Basketball Court/halfcourt/zone");
        //rim = GameObject.Find("/Basketball Court/backboard/rim");
        opp_positions = new Vector3[3];
        opp_positions[0] = opp0.transform.position;
        opp_positions[1] = opp1.transform.position;
        opp_positions[2] = opp2.transform.position;
        //initiate ball position randomly among the three places
        ball_origion();
        
        //ignore collision between default layer and zone layer
        Physics.IgnoreLayerCollision(0, 8);
		allSounds = GetComponents<AudioSource> ();
		bCheckResultRun = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(startTimer);

        //transform.position = Vector3.MoveTowards(transform.position, to, move_speed * Time.deltaTime);

        if (play_count && StartPlay)
        {
            play_time += 1 * Time.deltaTime;
        }

        //start timer before playing
        if (start_count)
        {
            if (startTimer > 0)
            {
                if (Mathf.Floor(startTimer) == 0)
                {
                    start_timerUI.GetComponent<Text>().text = "go!";
                }
                else
                {
					start_timerUI.GetComponent<Text>().text = Mathf.Floor(startTimer).ToString();

                }
                startTimer -= 1f * Time.deltaTime;


            }
            else
            {

                StartPlay = true; // start the game
                start_count = false;
                invi_canvas(start_canvas); //make the ui invisible
            }

        }

        //modify!
        if(StartPlay)
        {
            transform.position = Vector3.MoveTowards(transform.position, to, move_speed * Time.deltaTime);
			checkResult();
        }

    }



    void ball_origion()
    {

        //from_index = Random.Range(0, 3);
		//always start in the middle player	
		from_index = 1;
        from = opp_positions[from_index];
        transform.position = from;
        to_index = from_index;
        to = from;
        
    }

    /*
    void ball_origion2()
    {

    }
    */

    void ball_target()
    {

        if (pass)
        {
            int target;
            do
            {
                target = Random.Range(0, 3);
            }
            while (target == from_index);

            move_speed = pass_speed;
            to_index = target;
            to = opp_positions[to_index];

            if (from_index > to_index)
            {
                if (Mathf.Abs(from_index - to_index) > 1f)
                {
                    Anim[from_index].SetBool("passLeftLong", true);
                }
                else
                {
                    Anim[from_index].SetBool("passLeft", true);
                }
            }
            if (from_index < to_index)
            {
                if (Mathf.Abs(from_index - to_index) > 1f)
                {
                    Anim[from_index].SetBool("passRightLong", true);
                }
                else
                {
                    Anim[from_index].SetBool("passRight", true);
                }
            }



        }
        //fail to be in trigger area within certain amount of time, then shoot
        else
        {
            shooting();
        }
        //pass = false;

    }



    //when detect the ball within the to opp area, start new path
    //using trigger enter ball is more stable 

    IEnumerator OnTriggerEnter(Collider other)
    {
        //wait for the start time to redetect the trigger collider


        if (other.gameObject.name == "opp" + to_index)
        {
            Anim[from_index].SetBool("passLeft", false);
            Anim[from_index].SetBool("passRight", false);
            Anim[from_index].SetBool("passLeftLong", false);
            Anim[from_index].SetBool("passRightLong", false);

            from_index = to_index;
            from = opp_positions[from_index];
            transform.position = to;
            //wait while the start count down for moving at the beginning 


            if (!StartPlay)
            {
                //yield until user start to play
                yield return new WaitUntil(() => StartPlay == true);

            }

            Invoke("ball_target", delay_time);


        }
        //try not to change the pass within ontriggerenter or ontriggerstay   
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "opp" + from_index)
        {

            pass = false;
        }
    }



    void OnStart()
    {
        //StartPlay = true;
        //Debug.Log("Start Received " + StartPlay);
		if(player_script.getIsPlayerInTrigger()){
			start_count = true;
			allSounds[0].Play ();
		}
    }

    void OnPause()
    {
        PausePlay = true;
        //Debug.Log("Pause Received " + PausePlay);
        //Time.timeScale = 0.0f;
    }

    void OnResume()
    {
        PausePlay = false;
        //Debug.Log("Resume Received " + PausePlay);
        //Time.timeScale = 1.0f;
    }

    void OnSpeedUp()
    {
        this.pass_speed += 0.25f;
        Debug.Log("Pass speed changed up " + this.pass_speed);
    }

    void OnSlowDown()
    {
        this.pass_speed -= 0.25f;
        Debug.Log("Pass speed changed down " + this.pass_speed);
    }

    void OnRestart()
    {
        //Start();
        //player_script.Start();
        Reset();
        player_script.Reset();
    }

    void invi_canvas(CanvasGroup canvas)
    {
        canvas.alpha = 0f;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    void visi_canvas(CanvasGroup canvas)
    {
        canvas.alpha = 1f;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }

    void checkResult()
    {	
        if (play_time >= success_time && to_index != -1)
        {
            visi_canvas(result_canvas);
            result_UI.GetComponent<Text>().text = "training completed!";
			if (!bCheckResultRun) {
				allSounds [2].Play ();
				bCheckResultRun = true;
			}
            play_count = false;
        }
        else if (play_time <= success_time && to_index == -1)
        {
            visi_canvas(result_canvas);
            result_UI.GetComponent<Text>().text = "training failed!";
			if (!bCheckResultRun) {
				allSounds [1].Play ();
				bCheckResultRun = true;
			}
            play_count = false;
        }
    }
    //modify!
    private void Reset()
    {
        //stop looking for next target for ball when it is restarted
        CancelInvoke("ball_target");
        //reenter the same collider if the position didn't change to trigger the movement
        to = new Vector3(0, 0, 0);
        transform.position = to;
        //ball_origion();
        StartPlay = false;
        PausePlay = false;
        RestartPlay = false;
        start_count = false;
        play_time = 0f;
        play_count = true;
        pass = false;
        startTimer = 4f; //redefine the starttimer after restart
        //visi_canvas(start_canvas);
        start_timerUI.GetComponent<Text>().text = "start!"; //note the order of this line of code has to be afte visulize the timer_ui 
        invi_canvas(result_canvas);
        Invoke("ball_origion", 1f); 
		bCheckResultRun = false;
        //ball_origion();

    }

    void shooting()
    {

        to = rim.transform.position;
        move_speed = shoot_speed;
        to_index = -1;
    }

    

}
