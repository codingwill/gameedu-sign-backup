using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadPlayerPref : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Nama: " + PlayerPrefs.GetString("Nama"));
        Debug.Log("Username: " + PlayerPrefs.GetString("Username"));
        Debug.Log("Sekolah: " + PlayerPrefs.GetString("Sekolah"));
    }
}
