using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour {

    Vector3[] opp_positions;
    GameObject opp0;
    GameObject opp1;
    GameObject opp2;
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
    public bool pass;
    public bool count_down; //when ball in in collider, start to count down time and wait for player to be in trigger area
    
	// Use this for initialization
	void Start () {
        
        opp0 = GameObject.Find("/Basketball Court/halfcourt/opp0");
        opp1 = GameObject.Find("/Basketball Court/halfcourt/opp1");
        opp2 = GameObject.Find("/Basketball Court/halfcourt/opp2");
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
	void Update () {

        Debug.Log(pass);
        //move towards, distance? 
        transform.position = Vector3.MoveTowards(transform.position, to, move_speed * Time.deltaTime);

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
            to_index = target;
            to = opp_positions[to_index];
            
        }
        //fail to be in trigger area within certain amount of time, then shoot
        else
        {
            to = rim.transform.position;
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
            from_index = to_index;
            from = opp_positions[from_index];
            transform.position = to;
            Invoke("ball_target", delay_time);
            
            
        }
    }

    

}
