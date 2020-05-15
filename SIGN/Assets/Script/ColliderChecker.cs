using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{

    public bool collided = false;
    public bool correct = false;
    public string kode;
    Global sistemScript;
    public GameObject collidedObject;
    // Start is called before the first frame update
    void Start()
    {
        sistemScript = GameObject.Find("ScriptUtama").GetComponent<Global>();
    }
    public GameObject jawaban;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Sistem>().kode == kode)
        {
            collided = true;
            correct = true;
            collidedObject = other.gameObject;
        }
        else
        {
            collided = true;
            correct = false;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        collided = false;
        sistemScript.soundPlayed = false;
        if (collision.gameObject == jawaban)
        {
            collided = true;
        }
    }

}
