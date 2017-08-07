using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecevieAnimationController : MonoBehaviour {
	GameObject ball;
	private bool bAnimateReceived;
	ballController ball_script;


	// Use this for initialization
	void Start () {
		ball = GameObject.Find("/Basketball Court/halfcourt/ball");
		bAnimateReceived = false;
		ball_script = ball.GetComponent<ballController>();
	}
	
	// Update is called once per frame
	void Update () {
		animateReceive (ball, 1.8f);
		resetAnimateReceived (ball, 1.8f);
	}

	//reset the animate recevie boolean when blal out of range
	void resetAnimateReceived (GameObject b, float thresholdDist){
		if (Vector3.Distance (this.transform.position, b.transform.position) > thresholdDist) {
			bAnimateReceived = false;
		}
	}

	//if object is at certain distance from the player, play the animation in the direction
	void animateReceive (GameObject b, float thresholdDist){
		if (Vector3.Distance (this.transform.position, b.transform.position) < thresholdDist && !bAnimateReceived) {
			Debug.Log ("ball in range");
			bAnimateReceived = true;
			//if ball comes from right, play receive right animation, else play receive left
			if( relativeDir(this.transform, ball)){
				//receive right animation
				Debug.Log("Receive Right Animation");
				//this.GetComponent<Animator> ().SetTrigger("receive_r");
			}else{
				//receive left animation
				Debug.Log("Receive left Animation");
				//this.GetComponent<Animator> ().SetTrigger("receive_r");
			}
		}
	}

	//return relative irection between two given objects, Right = true, Left = false
	bool relativeDir(Transform origin, GameObject target){
		//Distance between origin and the target
		Vector3 distance = target.transform.position-origin.position;
		//relativepos: Right hand side = -z and all pos value, left hand side = -x and -x,-z value
		Vector3 relativePos = Vector3.zero;
		relativePos.x = Vector3.Dot (distance, origin.right.normalized);
		relativePos.y = Vector3.Dot (distance, origin.up.normalized);
		relativePos.z = Vector3.Dot (distance, origin.forward.normalized);
		Debug.Log(relativePos);
		if (relativePos.x > 0) {
			return true;
		} else {
			return false;
		}
	}
}
