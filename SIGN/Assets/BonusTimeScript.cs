using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusTimeScript : MonoBehaviour
{
    public GameObject sistemUtama, timer;
    public Text timerText;
    Text bonusText;
    Global sistemScript;
    // Start is called before the first frame update
    void Start()
    {
        sistemUtama = GameObject.Find("ScriptUtama");
        sistemScript = sistemUtama.GetComponent<Global>();
        timer = GameObject.Find("Timer");
        timerText = timer.GetComponent<Text>();
        bonusText = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        bonusText.text = "Bonus Time: " + (int) sistemScript.timeCounter * 2;

    }
}
