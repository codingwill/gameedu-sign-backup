using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sistemUtama, timer;
    public Text timerText;
    Text bonusText;
    Global sistemScript;
    ScoreCounter scoreCnt;
    // Start is called before the first frame update
    void Start()
    {
        sistemUtama = GameObject.Find("ScriptUtama");
        sistemScript = sistemUtama.GetComponent<Global>();
        timer = GameObject.Find("Timer");
        timerText = timer.GetComponent<Text>();
        bonusText = this.GetComponent<Text>();
        scoreCnt = GameObject.Find("Score").GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        bonusText.text = "Total Score: " + (((int)sistemScript.timeCounter * 2) + scoreCnt.score);
    }
}
