using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkorAkhir : MonoBehaviour
{
    Text thisText;
    int totalTime = 0;
    int totalFinish = 0;
    // Start is called before the first frame update
    void Start()
    {
        thisText = this.gameObject.GetComponent<Text>();
        /*
        PlayerPrefs.SetInt("TimeScore1", 0);
        PlayerPrefs.SetInt("TimeScore2", 0);
        PlayerPrefs.SetInt("TimeScore3", 0);
        PlayerPrefs.SetInt("TimeScore4", 0);
        PlayerPrefs.SetInt("TimeScore5", 0);
        PlayerPrefs.SetInt("TimeScore6", 0);
        PlayerPrefs.SetInt("TimeScore7", 0);

        PlayerPrefs.SetInt("FinishScore1", 0);
        PlayerPrefs.SetInt("FinishScore2", 0);
        PlayerPrefs.SetInt("FinishScore3", 0);
        PlayerPrefs.SetInt("FinishScore4", 0);
        PlayerPrefs.SetInt("FinishScore5", 0);
        PlayerPrefs.SetInt("FinishScore6", 0);
        PlayerPrefs.SetInt("FinishScore7", 0);
        */
        totalTime = (PlayerPrefs.GetInt("TimeScore1") + PlayerPrefs.GetInt("TimeScore2") + PlayerPrefs.GetInt("TimeScore3")
                    + PlayerPrefs.GetInt("TimeScore3") + PlayerPrefs.GetInt("TimeScore4") + PlayerPrefs.GetInt("TimeScore5")
                    + PlayerPrefs.GetInt("TimeScore6") + PlayerPrefs.GetInt("TimeScore7"));

        totalFinish = (PlayerPrefs.GetInt("FinishScore1") + PlayerPrefs.GetInt("FinishScore2") + PlayerPrefs.GetInt("FinishScore3")
                    + PlayerPrefs.GetInt("FinishScore3") + PlayerPrefs.GetInt("FinishScore4") + PlayerPrefs.GetInt("FinishScore5")
                    + PlayerPrefs.GetInt("FinishScore6") + PlayerPrefs.GetInt("FinishScore7"));
        submitScore();
    }

    // Update is called once per frame
    void Update()
    {


        thisText.text = "" + (totalTime + totalFinish);
        Debug.Log("TimeScore2 = " + PlayerPrefs.GetInt("TimeScore2"));
    }

    void submitScore()
    {
        string user = PlayerPrefs.GetString("Username");
        string nama = PlayerPrefs.GetString("Nama");
        string sekolah = PlayerPrefs.GetString("Sekolah");
        string url = "https://us-central1-sign-e15cc.cloudfunctions.net/addScore?username=" + user + "&finishscore=" + (totalFinish) + "&timescore=" + (totalTime);
        WWW www = new WWW(url);
        StartCoroutine(kirimData(www));
    }
    IEnumerator kirimData(WWW www)
    {
        //StartCoroutine(wait(5));
        yield return www;
    }
}
