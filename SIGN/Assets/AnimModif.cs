using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimModif : MonoBehaviour
{
    // Start is called before the first frame update
    Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 0, 255f);
        //ani.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void animate()
    {
        this.GetComponent<Animator>().enabled = true;
    }
}
