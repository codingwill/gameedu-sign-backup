using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pembacaSoal : MonoBehaviour
{
    public GameObject sistemUtama;
    Global sistemScript;
    AudioSource audioPlayer;
    public AudioClip audioSoal, audioHint;
    // Start is called before the first frame update
    void Start()
    {
        sistemUtama = GameObject.Find("ScriptUtama");
        sistemScript = sistemUtama.GetComponent<Global>();
        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = audioSoal;
    }

    private void OnMouseDown()
    {
        bacaSoal();
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(bacaHint(5f));    
    }

    void bacaSoal()
    {
        audioPlayer.Stop();
        audioPlayer.clip = audioSoal;
        audioPlayer.Play();
    }
    IEnumerator bacaHint(float jeda)
    {
        yield return new WaitForSeconds(jeda);
        audioPlayer.Stop();
        audioPlayer.clip = audioHint;
        audioPlayer.Play();   
    }
}
