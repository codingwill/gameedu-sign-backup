using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelRestriction : MonoBehaviour
{
    public int minimalLevel;
    Button thisButton;
    // Start is called before the first frame update
    void Start()
    {
        thisButton = this.gameObject.GetComponent<Button>();
        thisButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        int unlock = PlayerPrefs.GetInt("Level");
        Debug.Log("unlock = " + unlock);
        if (unlock >= minimalLevel)
        {
            thisButton.enabled = true;
        }
    }
}
