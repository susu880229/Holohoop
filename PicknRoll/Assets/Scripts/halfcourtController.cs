using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class halfcourtController : MonoBehaviour {

    Vector3 position1;
    Vector3 position2;
    float x1, y1, z1;
    float x2, y2, z2; //define the two points position
    float k, b; //the parameter for the line
    private GameObject player;
    Color c1; //red
    Color c2; //green
    Color c3; //blue
    private Vector3 player_position;
    bool posi1_com;
    bool arrow_com;
    float offset;

	// Use this for initialization
	void Start () {

        x1 = 0f;
        y1 = 0.5f;
        z1 = 2f;
        x2 = -2f;
        y2 = 0.5f;
        z2 = 4f;
        offset = 0.5f;
        k = (z2 - z1) / (x2 - x1);
        b = (x2 * z1 - x1 * z2)/ (x2 - x1);
        position1 = new Vector3(x1, y1, z1);
        position2 = new Vector3(x2, y2, z2);
        player = GameObject.FindGameObjectWithTag("MainCamera");
        c1 = new Color32(255, 82, 82, 255); //red
        c2 = new Color32(30, 255, 0, 255);  //green
        c3 = new Color32(59, 0, 255, 255); //blue
        transform.Find("position1").gameObject.GetComponent<SpriteRenderer>().color = c1;
        posi1_com = false;
        arrow_com = false;
        
    }

    // Update is called once per frame
    void Update() {
        

        //detect if player are in the position1 area
        player_position = player.transform.position;
        if (inArea(position1))
        {
            transform.Find("position1").gameObject.GetComponent<SpriteRenderer>().color = c2;
            posi1_com = true;
            transform.Find("arrow").gameObject.SetActive(true);
        }
       
        
        //detect if the player are in the arrow
        if (posi1_com == true)
        {
            if (player_position.z >= k * player_position.x + b - offset && player_position.z <= k * player_position.x + b + offset)
            {
                //define x range
                if (player_position.z >= position1.z - offset && player_position.z <= position2.z + offset)
                {
                    //arrow feedback 
                    transform.Find("arrow").gameObject.GetComponent<SpriteRenderer>().color = c2;

                    //detect if the player is in the position2 area only if the player finish the arrow
                    if (inArea(position2))
                    {
                        transform.Find("position2").gameObject.SetActive(true);
                        //transform.Find("position1").gameObject.SetActive(false);
                        //transform.Find("arrow").gameObject.SetActive(false);
                        arrow_com = true;
                        
                    }

                }

                //exceed the length of the line
                else
                {
                    redo();
                }
               
                
            }
            //not on the designated line direction
            else
            {
                redo();
            }
        }

    }

    //redo the path if not complete
    void redo() {
        arrow_com = false;
        transform.Find("position1").gameObject.GetComponent<SpriteRenderer>().color = c1;
        transform.Find("arrow").gameObject.SetActive(false);
        transform.Find("position2").gameObject.SetActive(false);
    }

    //check if the player is within the designated area
    bool inArea(Vector3 point_position) {

        return player_position.x > point_position.x - offset && player_position.x < point_position.x + offset && player_position.z > point_position.z - offset && player_position.z < point_position.z + offset;

    }

}
