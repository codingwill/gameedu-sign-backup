using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    float time;
    GameObject sistemUtama;
    Global sistemScript;
    Text timerText;
    int timer;
    // Start is called before the first frame update
    void Start()
    {
        sistemUtama = GameObject.Find("ScriptUtama");
        sistemScript = sistemUtama.GetComponent<Global>();
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time = sistemScript.timeCounter;
        //Debug.Log(time);
        timer = (int) time;
        timerText.text = "Waktu: " + timer;
    }
}
