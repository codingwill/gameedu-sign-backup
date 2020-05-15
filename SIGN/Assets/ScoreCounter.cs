using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int score = 0;
    int nilaiBenar = 100;
    int nilaiSalah = -5;
    int nilaiMelenceng = -5;
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public void addScore()
    {
        score += nilaiBenar;
        PlayerPrefs.SetInt("FinishScore", PlayerPrefs.GetInt("FinishScore") + nilaiBenar);
    }

    public void salahScore()
    {
        score += nilaiSalah;
        PlayerPrefs.SetInt("FinishScore", PlayerPrefs.GetInt("FinishScore") + nilaiSalah) ;
    }

    public void melencengScore()
    {
        score += nilaiMelenceng;
        PlayerPrefs.SetInt("FinishScore", PlayerPrefs.GetInt("FinishScore") + nilaiMelenceng);
    }
}
