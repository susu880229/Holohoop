using UnityEngine;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity.InputModule;

public class TapToRestart : MonoBehaviour {
	

	// Called by GazeGestureManager when the user performs a Select gesture
	void  OnSelect() {
		GameObject b =GameObject.Find("/Basketball Court/halfcourt/ball");
		GameObject p =GameObject.Find("/Main Camera");
		Debug.Log ("Hello");
		b.GetComponent<ballController> ().Start();
		p.GetComponent<playerController> ().Start();

	}
}
