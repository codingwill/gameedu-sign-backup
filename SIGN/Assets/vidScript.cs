using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class vidScript : MonoBehaviour
{
    // Start is called before the first frame update
    VideoPlayer vp;
    void Start()
    {
        vp = this.GetComponent<VideoPlayer>();
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, "3 an MB Anabolisme.mp4");
    }

    // Update is called once per frame
    void Update()
    {
        //vp.frame = 120;
    }
}
