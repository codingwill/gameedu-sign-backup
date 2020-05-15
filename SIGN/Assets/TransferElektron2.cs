using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferElektron2 : MonoBehaviour
{
    Global sistemScript;
    Animator thisBola;
    // Start is called before the first frame update
    void Start()
    {
        sistemScript = GameObject.Find("ScriptUtama").GetComponent<Global>();
        thisBola = GetComponent<Animator>();
        PlayerPrefs.SetInt("transpor", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (sistemScript.soal == 2 && PlayerPrefs.GetInt("transpor") == 1)
        {
            this.gameObject.GetComponent<Animator>().enabled = true;
            if (thisBola.GetCurrentAnimatorStateInfo(0).IsName("Exit"))
            {
                PlayerPrefs.SetInt("transpor", PlayerPrefs.GetInt("transpor") + 1);
            }
        }
    }
}
