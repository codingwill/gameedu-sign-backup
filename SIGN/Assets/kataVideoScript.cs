using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class kataVideoScript : MonoBehaviour
{
    public string namaVideo;
    VideoPlayer vp;
    VideoClip vc;
    // Start is called before the first frame update
    void Start()
    {
        vp = this.GetComponent<VideoPlayer>();
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, namaVideo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
