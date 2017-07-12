﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        keywords.Add("Reset world", () =>
        {
            // Call the OnReset method on every descendant object.
            this.BroadcastMessage("OnReset");
        });

        keywords.Add("Drop Sphere", () =>
        {
            var focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                // Call the OnDrop method on just the focused object.
                focusObject.SendMessage("OnDrop");
            }
        });

        keywords.Add("Start", () =>
        {
            this.BroadcastMessage("OnStart");
            Debug.Log("Start");
        });

        keywords.Add("Pause", () =>
        {
            this.BroadcastMessage("OnPause");
            Debug.Log("Pause");

        });

        keywords.Add("Resume", () =>
        {
            this.BroadcastMessage("OnResume");
            Debug.Log("Resume");

        });

        keywords.Add("Restart", () =>
        {
            this.BroadcastMessage("OnRestart");
            Debug.Log("Restart");
            
        });

        keywords.Add("Speed Up", () =>
        {
            this.BroadcastMessage("OnSpeedUp");
            Debug.Log("Speed Up");

        });

        keywords.Add("Slow Down", () =>
        {
            this.BroadcastMessage("OnSpeedDown");
            Debug.Log("Slow Down");

        });

        keywords.Add("Scale Down", () =>
        {
            this.BroadcastMessage("OnScaleDown");
            Debug.Log("Scale Down");

        });

        keywords.Add("Scale Up", () =>
        {
            this.BroadcastMessage("OnScaleUp");
            Debug.Log("Scale Up");

        });
        keywords.Add("Higher Up", () =>
        {
            this.BroadcastMessage("OnHigherUp");
            Debug.Log("Higher Up");

        });
        keywords.Add("Lower Down", () =>
        {
            this.BroadcastMessage("OnLowerDown");
            Debug.Log("Lower Down");

        });
        keywords.Add("Visual Aid", () =>
        {
            this.BroadcastMessage("OnVisualAid");
            Debug.Log("Visual Aid");

        });
        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.E))
		{
			this.BroadcastMessage("OnRestart");
		}
		if (Input.GetKeyDown(KeyCode.S))
        {
            this.BroadcastMessage("OnStart");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.BroadcastMessage("OnPause");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.BroadcastMessage("OnResume");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.BroadcastMessage("OnSpeedUp");
        }
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}