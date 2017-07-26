using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballController : MonoBehaviour
{

    public Vector3[] opp_positions;
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
    //Added UI images
    GameObject result_UI_image;
    //Adding Ended
    playerController player_script;
    Vector3 from;
    public int from_index;
    public Vector3 to;
    public int to_index;
    public float move_speed = 1f;
    public float delay_time = 3f;
    public float shoot_speed = 6f;
    public float pass_speed = 1f;
	public float ball_speed=0.6f;
    public bool pass;

    //start timer varibles
    public float startTimer = 4f;
    public bool start_count;

    //successful drill varibles
    public float success_time = 45f;
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
    bool first_trigger;

    public Sprite SUCCESS, FAIL;
    

    // Use this for initialization
    public void Start()
    {
        
        StartPlay = false;
        PausePlay = false;
        RestartPlay = false;
        start_count = false;
        first_trigger = true;
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
        result_UI_image = GameObject.Find("Main Camera/Result_UI/ResultIcon");
        //start_timerUI.GetComponent<Text>().text = startTimer.ToString();
        start_timerUI.GetComponent<Text>().fontSize = 12;
        start_timerUI.GetComponent<Text>().text = "START";
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
		//ignore the old rim collider for the ball to be rest in the rim when shot 
		rim.GetComponent<Collider> ().isTrigger = true;

        //SUCCESS = Resources.Load<Sprite>("Resources/UIElements/UI_Checkmark.png") as Sprite;
        //FAIL = Resources.Load<Sprite>("Resources/UIElement/UI_X.png") as Sprite;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ball:" + Anim[1].GetBool("ball").ToString() + "pass:" + Anim[1].GetBool("pass").ToString() + "left:" + Anim[1].GetBool("pass_left").ToString() + "short:" + Anim[1].GetBool("short").ToString());

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
                    start_timerUI.GetComponent<Text>().fontSize = 24;
                    start_timerUI.GetComponent<Text>().text = "GO";
                }
                else
                {
                    start_timerUI.GetComponent<Text>().fontSize = 24;
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

        
        if(StartPlay)
        {
			// if the to position ! = the previous to position, then call Launch Once
			if (to != from) {
				if (to == rim.transform.position) {
					Launch (transform.position, to, 30f);
				} else {
					Launch (transform.position, to, 12f);
				}
			}

			checkResult();
        }
		//Tell the player how much time is left
		if(Mathf.Floor(play_time) == (success_time- 40f)){
			allSounds [6].Play ();
		}
		if(Mathf.Floor(play_time) == (success_time- 30f)){
			allSounds [5].Play ();
		}
		if(Mathf.Floor(play_time) == (success_time- 20f)){
			allSounds [4].Play ();
		}
		if(Mathf.Floor(play_time) == (success_time- 10f)){
			allSounds [3].Play ();
		}

		//Debug.Log (Mathf.Floor(play_time));
    }

	//projectile motion launch
	void Launch(Vector3 curPos, Vector3 toPos, float fangle){
		//Physics.IgnoreCollision (GetComponent<Collider> (), player_script.GetComponent<Collider> ());
		//distance between current player position and the target position
		float fdist = Vector3.Distance(curPos,toPos);
		transform.LookAt (toPos);
		//calculate initial velocity for ball to land on the next player
		float Vinitial = Mathf.Sqrt(fdist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * fangle * 2)));
		//Vy and Vz component of initial velocity
		float Vy = Vinitial * Mathf.Sin(Mathf.Deg2Rad * fangle);
		float Vz = Vinitial * Mathf.Cos(Mathf.Deg2Rad * fangle);

		//local space velocity
		Vector3 localV = new Vector3(0f,Vy,Vz);
		//global velocity vector (0.7f is hard coded so this disalbes the user from chagnign ball speed to sync with animation)
		Vector3 globalV = transform.TransformDirection(0.7f*localV);
		//launch the ball to set initial velocity
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().velocity = globalV;
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
        Anim[to_index].SetBool("ball", true);
    }

   

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

            
            //pass to the left and receive from right
            if (from_index > to_index)
            {
                
                Anim[to_index].SetBool("receive_r", true);

                if (Mathf.Abs(from_index - to_index) > 1f)
                {
                    Anim[from_index].SetBool("long_p_l", true);
                    
                }
                else
                {
                    Anim[from_index].SetBool("short_p_l", true);
                    
                }
            }

            //pass to the right and receive from left
            if (from_index < to_index)
            {
                //set the animation to pass and receive
                
                Anim[to_index].SetBool("receive_l", true);

                if (Mathf.Abs(from_index - to_index) > 1f)
                {
                    Anim[from_index].SetBool("long_p_r", true);
                   
                }
                else
                {
                    Anim[from_index].SetBool("short_p_r", true);
                    
                }
            }

            

        }
        //fail to be in trigger area within certain amount of time, then shoot
        else
        {
            shooting();
            Anim[from_index].SetBool("shoot", true);
        }
        

    }



    //when detect the ball within the to opp area, start new path
    //using trigger enter ball is more stable 

    IEnumerator OnTriggerEnter(Collider other)
    {
		//"catch" the ball by turning gravity on
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().useGravity = false;
        //wait for the start time to redetect the trigger collider
	
        if (other.gameObject.name == "opp" + to_index)
        {
            
            //test animation logic to reset condition
            reset_anim();

            from_index = to_index;
            from = opp_positions[from_index];
            transform.position = to;

            //wait while the start count down for moving at the beginning 
            if (!StartPlay)
            {
                //yield until user start to play
                yield return new WaitUntil(() => StartPlay == true);
                
            }

            //make sure there is no delay at the first defense trigger zone
            if(first_trigger)
            {
                ball_target();
                first_trigger = false;
            }
            else
            {
                Invoke("ball_target", delay_time);
            }
            


        }
        //try not to change the pass within ontriggerenter or ontriggerstay   
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "opp" + from_index)
        {

            pass = false;
            //reset from player receive status 
            Anim[from_index].SetBool("receive_l", false);
            Anim[from_index].SetBool("receive_r", false);

        }
    }



    void OnStart()
    {
        
		if(player_script.getIsPlayerInTrigger()){
			start_count = true;
            //start to play the animation 
            foreach (Animator anim in Anim)
            {
                anim.SetBool("start", true);
            }
            allSounds[0].Play ();
		}
		player_script.walkThruClips [0].Stop();
		player_script.setExitWalkThru (true);
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
        this.ball_speed += 0.1f;
		Debug.Log("Pass speed changed up " + this.ball_speed);
    }

    void OnSlowDown()
    {
		this.ball_speed -= 0.1f;
		Debug.Log("Pass speed changed down " + this.ball_speed);
    }

    void OnRestart()
    {
        Reset();
        player_script.Reset();
    }
	//"say repeat" for repeating the voice waklthrough, once the walkthru is exited, CAN NOT repeat again
	void OnRepeat()
	{
		if (!player_script.getExitWalkThru ()) {
			player_script.playWalkthrough ();
		}

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
            result_UI.GetComponent<Text>().text = "SUCCESS";
            result_UI_image.GetComponent<Image>().sprite = SUCCESS;
			if (!bCheckResultRun) {
				allSounds [2].Play ();
				bCheckResultRun = true;
			}
            play_count = false;
        }
        else if (play_time <= success_time && to_index == -1)
        {
            visi_canvas(result_canvas);
            result_UI.GetComponent<Text>().text = "FAIL";
            result_UI_image.GetComponent<Image>().sprite = FAIL;
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
        StartPlay = false;
        PausePlay = false;
        RestartPlay = false;
        start_count = false;
        play_time = 0f;
        play_count = true;
        pass = false;
        startTimer = 4f; //redefine the starttimer after restart
        //visi_canvas(start_canvas);
        start_timerUI.GetComponent<Text>().fontSize = 12;
        start_timerUI.GetComponent<Text>().text = "START"; //note the order of this line of code has to be afte visulize the timer_ui 
        invi_canvas(result_canvas);
        restart_anim();
        Invoke("ball_origion", 1f); 
		bCheckResultRun = false;
        first_trigger = true;
        player_script.bIsPlayerInTrigger = false;
        

    }

    //excecute once when restart , need to test 
    void restart_anim()
    {
        //restart animation
        foreach (Animator anim in Anim)
        {
            if (!anim.isInitialized)
            {
                anim.Rebind();
            }

        }
    }
    
    //excecute every time ball is within the trigger area
    void reset_anim()
    {
        
        //reset from player
        //Anim[from_index].SetBool("ball", false);
        Anim[from_index].SetBool("short_p_l", false);
        Anim[from_index].SetBool("long_p_l", false);
        Anim[from_index].SetBool("short_p_r", false);
        Anim[from_index].SetBool("long_p_r", false);
        Anim[from_index].SetBool("shoot", false);

        //reset to player
        //Anim[to_index].SetBool("ball", true);
        
        

        
    }
    void shooting()
    {

        to = rim.transform.position;
        move_speed = shoot_speed;
        to_index = -1;
    }

	public bool GetStartCount(){
		return start_count;
	}

    

}
