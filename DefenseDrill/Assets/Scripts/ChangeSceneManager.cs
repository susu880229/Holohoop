using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour {

	void OnSelect(){
		LoadScene ();
	}

	void LoadScene(){
		SceneManager.LoadScene (1);
	}

}
