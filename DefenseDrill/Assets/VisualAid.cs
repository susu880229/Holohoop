using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAid : MonoBehaviour {
    private bool va = false;
    private bool tempVA;

    private Renderer[] rd;

	// Use this for initialization
	void Start () {
        tempVA = va;
        rd = this.GetComponentsInChildren<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            va = !va;
            if (tempVA != va)
            {
                foreach (Renderer r in rd) {
                    r.enabled = va;
                }

                tempVA = va;
            }
        }
	}

    void OnVisualAid()
    {
        va = !va;
        if (tempVA != va)
        {
            foreach (Renderer r in rd)
            {
                r.enabled = va;
            }

            tempVA = va;
        }
    }

    
}
