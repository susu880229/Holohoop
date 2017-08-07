using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour {

    private CanvasGroup RestartUI;

    // Use this for initialization
    void Start () {
        RestartUI = GameObject.Find("Main Camera/Restart_UI").GetComponent<CanvasGroup>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnMenu()
    {
        if(RestartUI.alpha > 0)
        {
            //Application.loadedLevel();
        }
    }
}
