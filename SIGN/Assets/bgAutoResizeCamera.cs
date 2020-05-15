using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgAutoResizeCamera : MonoBehaviour
{

    public GameObject cam;
    Vector3 posUpdater;
    // Start is called before the first frame update
    void Start()
    {

        cam = GameObject.FindWithTag("MainCamera");
        posUpdater = cam.transform.position;
        posUpdater.z = 16.0f;
    }

    // Update is called once per frame
    void Update()
    {
        posUpdater.x = cam.transform.position.x;
        posUpdater.y = cam.transform.position.y;
        this.transform.position = posUpdater;
    }
}
