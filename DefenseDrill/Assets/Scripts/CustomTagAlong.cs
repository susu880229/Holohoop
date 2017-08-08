using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTagAlong : MonoBehaviour {
	GameObject oUIPanel;

	// Use this for initialization
	void Start () {
		oUIPanel = GameObject.Find("/SpeechManager/UIPanel");

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vUIPanelPos = oUIPanel.transform.position;
		float fThresDist;
		float fDistPanelTagobj = (vUIPanelPos - this.transform.position).magnitude;
		//Debug.Log (fDistPanelTagobj);
		if (isTopOrBottomOf (this.transform, oUIPanel)) {
			//tagalong ojbect on either top or bottom, so use another value for threhold as width of panel is < length of panel
			//Debug.Log("Vertical");
			fThresDist = 0.3f;
		} else {
			//tagalong ojbect on either left or right
			//Debug.Log("Horizontal");
			fThresDist = 0.5f;
		}
		float MovementSpeed = Vector3.Distance (oUIPanel.transform.position, transform.position) / 0.3f; 
		if (fDistPanelTagobj > fThresDist) {
			oUIPanel.transform.position = Vector3.MoveTowards (oUIPanel.transform.position, transform.position, MovementSpeed * Time.deltaTime);
		}



	}

	//return relative direction between two given objects, if it's on top or bottom of object, return true
	bool isTopOrBottomOf(Transform origin, GameObject target){
		//Distance between origin and the target
		Vector3 distance = target.transform.position-origin.position;
		//relativepos: Right hand side = -z and all pos value, left hand side = -x and -x,-z value
		Vector3 relativePos = Vector3.zero;
		relativePos.x = Vector3.Dot (distance, origin.right.normalized);
		relativePos.y = Vector3.Dot (distance, origin.up.normalized);
		relativePos.z = Vector3.Dot (distance, origin.forward.normalized);
		//Debug.Log(relativePos);
		if (relativePos.x < 0.5f && relativePos.x > -0.5f && (relativePos.y > 0.27f || relativePos.y < -0.27f)) {
			return true;
		} else {
			return false;
		}
	}
}
