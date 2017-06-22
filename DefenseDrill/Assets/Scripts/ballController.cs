using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour {

    Vector3[] opp_positions;
    GameObject opp0;
    GameObject opp1;
    GameObject opp2;
    GameObject player0;
    GameObject player1;
    GameObject player2;
    GameObject zone;
    GameObject rim;
    GameObject player;
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
    public bool count_down; //when ball in in collider, start to count down time and wait for player to be in trigger area

    private bool StartPlay;
    private bool PausePlay;
    private bool RestartPlay;

    private Animator[] Anim;

    // Use this for initialization
    public void Start () {

        StartPlay = false;
        PausePlay = false;
        RestartPlay = false;

        opp0 = GameObject.Find("/Basketball Court/halfcourt/opp0");
        opp1 = GameObject.Find("/Basketball Court/halfcourt/opp1");
        opp2 = GameObject.Find("/Basketball Court/halfcourt/opp2");

        player0 = GameObject.Find("/Basketball Court/halfcourt/player0");
        player1 = GameObject.Find("/Basketball Court/halfcourt/player1");
        player2 = GameObject.Find("/Basketball Court/halfcourt/player2");

        Anim = new Animator[3];
        Anim[0] = player0.GetComponent<Animator>();
        Anim[1] = player1.GetComponent<Animator>();
        Anim[2] = player2.GetComponent<Animator>();

        zone = GameObject.Find("/Basketball Court/halfcourt/zone");
        rim = GameObject.Find("/Basketball Court/backboard/rim");
        opp_positions = new Vector3[3];
        opp_positions[0] = opp0.transform.position;
        opp_positions[1] = opp1.transform.position;
        opp_positions[2] = opp2.transform.position;
        //initiate ball position randomly among the three places
        ball_origion();
        //ignore collision between default layer and zone layer
        Physics.IgnoreLayerCollision(0, 8);
        pass = false;
        count_down = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(pass);
        //move towards, distance? 
        if (StartPlay && !PausePlay) { 
            transform.position = Vector3.MoveTowards(transform.position, to, move_speed * Time.deltaTime);
        }
    }
  

    void ball_origion()
    {
        
        from_index = Random.Range(0, 3);
        from = opp_positions[from_index];
        transform.position = from;
        to_index = from_index;
        to = from;

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
            if(from_index > target)
            {
                Anim[from_index].SetBool("passLeft", true);
            }
            if(from_index < target)
            {
                Anim[from_index].SetBool("passRight", true);
            }
            to_index = target;
            to = opp_positions[to_index];
            
        }
        //fail to be in trigger area within certain amount of time, then shoot
        else
        {
            to = rim.transform.position;
            move_speed = shoot_speed;
            to_index = -1;
        }
        pass = false;
    }

    

    //when detect the ball within the to opp area, start new path
    //using trigger enter ball is more stable 
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "opp" + to_index)
        {
            Anim[from_index].SetBool("passLeft", false);
            Anim[from_index].SetBool("passRight", false);

            from_index = to_index;
            from = opp_positions[from_index];
            transform.position = to;
            Invoke("ball_target", delay_time);
            
           
        }
    }

    void OnStart()
    {
        StartPlay = true;
        Debug.Log("Start Received " + StartPlay);
    }

    void OnPause()
    {
        PausePlay = true;
        Debug.Log("Pause Received " + PausePlay);
    }

    void OnResume()
    {
        PausePlay = false;
        Debug.Log("Resume Received " + PausePlay);
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





}
