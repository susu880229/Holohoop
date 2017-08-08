using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPosition : MonoBehaviour
{


    public Canvas ResultUI;
    public Canvas TimerUI;
    public Canvas RestartUI;

    public float offset;

    private RectTransform RectRs;
    private RectTransform RectTm;
    private RectTransform RectRt;

    private float initialRsZ;
    private float initialTmZ;
    private float initialRtZ;

    private Vector3 RsScale;
    private Vector3 TmScale;
    private Vector3 RtScale;

    private Vector3 initialRsS;
    private Vector3 initialTmS;
    private Vector3 initialRtS;

    private float shorten;
    private Vector3 CameraPos;

    public float factor;

    // Use this for initialization
    void Start()
    {

        //initialRsZ = ResultUI.transform.position.z;
        //initialTmZ = TimerUI.transform.position.z;

        RectRs = ResultUI.GetComponent<RectTransform>();
        RectTm = TimerUI.GetComponent<RectTransform>();
        RectRt = RestartUI.GetComponent<RectTransform>();

        RsScale = RectRs.localScale;
        TmScale = RectTm.localScale;

        initialRsZ = RectRs.localPosition.z / factor;
        initialTmZ = RectTm.localPosition.z / factor;
        initialRtZ = RectRt.localPosition.z / factor;

        shorten = 10f;


    }

    // Update is called once per frame
    void Update()
    {
        CameraPos = Camera.main.transform.position;
        this.transform.position = new Vector3(CameraPos.x, CameraPos.y, CameraPos.z + offset);
        if(shorten < 10f)
        {
            //Debug.Log("local pos " + RectRs.localPosition.z + " limit " + (initialRsZ - shorten));
            if (RectRs.localPosition.z >= (initialRsZ - shorten))
            {
                RectRs.localPosition = new Vector3(RectRs.localPosition.x, RectRs.localPosition.y, RectRs.localPosition.z - 1f * Time.deltaTime);
                RectTm.localPosition = new Vector3(RectTm.localPosition.x, RectTm.localPosition.y, RectTm.localPosition.z - 1f * Time.deltaTime);
                RectRt.localPosition = new Vector3(RectRt.localPosition.x, RectRt.localPosition.y, RectRt.localPosition.z - 1f * Time.deltaTime);
                //RectRs.localScale = RectRs.localScale * (11/10) * 1f * Time.deltaTime;
                //RectTm.localScale = RectTm.localScale * (11 / 10) * 1f * Time.deltaTime;
            }
        }
        if(shorten > 10f)
        {
            //Debug.Log("local pos " + RectRs.localPosition.z  + " initial z " + initialRsZ);
            if (RectRs.localPosition.z <= initialRsZ)
            {
                RectRs.localPosition = new Vector3(RectRs.localPosition.x, RectRs.localPosition.y, RectRs.localPosition.z + 1f * Time.deltaTime);
                RectTm.localPosition = new Vector3(RectTm.localPosition.x, RectTm.localPosition.y, RectTm.localPosition.z + 1f * Time.deltaTime);
                RectRt.localPosition = new Vector3(RectRt.localPosition.x, RectRt.localPosition.y, RectRt.localPosition.z + 1f * Time.deltaTime);
                //RectRs.localScale = RectRs.localScale * (10 / 11) * 1f * Time.deltaTime;
                //RectTm.localScale = RectTm.localScale * (10 / 11) * 1f * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Get in the way " + other.gameObject.name);
        if (other.gameObject.name == "triggerui")
        {
        
        Vector3 vectorDis = Camera.main.transform.position - other.gameObject.transform.position;

        Vector3 midPt = vectorDis / 2;
        float midPtZ = midPt.z;
        float dis = midPt.magnitude;
        shorten = dis / 2;
        //Debug.Log("Shorten " + shorten);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        shorten = 20f;
        
    }


}
