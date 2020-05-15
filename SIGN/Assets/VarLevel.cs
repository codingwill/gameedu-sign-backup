using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarLevel : MonoBehaviour
{
    public string finishKey, timeKey;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt(finishKey, 0);
        PlayerPrefs.SetInt(timeKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addFinishScore(int score)
    {
        PlayerPrefs.SetInt(finishKey, score);
    }
    public void addTimeScore(int score)
    {
        PlayerPrefs.SetInt(timeKey, score);
    }
}
