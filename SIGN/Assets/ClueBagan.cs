using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBagan : MonoBehaviour
{
    SpriteRenderer bagan;
    Global sistem;
    public int soal;
    // Start is called before the first frame update
    void Start()
    {
        bagan = this.gameObject.GetComponent<SpriteRenderer>();
        bagan.enabled = false;
        sistem = GameObject.Find("ScriptUtama").GetComponent<Global>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sistem.soal + 1 >= soal)
        {
            bagan.enabled = true;
        }
    }
}
