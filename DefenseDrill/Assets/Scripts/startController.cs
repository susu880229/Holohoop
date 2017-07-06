using UnityEngine;
using UnityEngine.SceneManagement;

public class startController : MonoBehaviour
{
    GameObject ball;
    GameObject player;
    ballController ball_script;
    playerController player_script;
    CanvasGroup canvas;
    bool receive = false;
    
    private void Awake()
    {
        ball = GameObject.Find("/Basketball Court/halfcourt/ball");
        player = GameObject.Find("Main Camera");
        ball_script = ball.GetComponent<ballController>();
        player_script = player.GetComponent<playerController>();
        //canvas = transform.parent.gameObject.GetComponent<CanvasGroup>();
    }



    // Called by GazeGestureManager when the user performs a Select gesture
    private void Update()
    {
        /*
        if (Input.GetKeyDown("space"))
        {
            Restart();
        }
        */
        //Debug.Log("receive" + receive);
    }
    public void Restart()
    {
        receive = true;
        ball_script.Start();
        player_script.Start();
        
        //canvas.alpha = 0f;
        //canvas.interactable = false;
        //canvas.blocksRaycasts = false;


    }



}