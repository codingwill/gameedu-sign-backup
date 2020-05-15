using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public GameObject selAns, selTanya;
    GameObject[] ans, tanya, initAns;
    GameObject controller;
    int soal = 0, ansCount = 5;
    Vector3 touchPosInit;
    RaycastHit2D raycastInfo;
    Vector3 controlPosInit;
    Vector3[] ansPos;
    float jarakAns = 1.5f;
    public bool sedangMenarik;
    // Start is called before the first frame update
    void Start()
    {
        ansPos = new Vector3[ansCount];
        for (int i = 0; i < 5; i++)
        {
            ansPos[i] = new Vector3(-9.5f, (i * jarakAns) - 3.0f);
        }
        Shuffle(ansPos);
        ans = new GameObject[5];
        initAns = new GameObject[5];
        tanya = new GameObject[5];
        controller = GameObject.Find("controlJawab");
        soal = 0; //soal pertama dimulai dengan angka 0
        ans[0] = GameObject.Find("ans1");
        ans[1] = GameObject.Find("ans2");
        ans[2] = GameObject.Find("ans3");
        ans[3] = GameObject.Find("ans4");
        ans[4] = GameObject.Find("ans5");
        initAns[0] = GameObject.Find("initPosAns1");
        initAns[1] = GameObject.Find("initPosAns2");
        initAns[2] = GameObject.Find("initPosAns3");
        initAns[3] = GameObject.Find("initPosAns4");
        initAns[4] = GameObject.Find("initPosAns5");
        tanya[0] = GameObject.Find("tanya1");
        tanya[1] = GameObject.Find("tanya2");
        tanya[2] = GameObject.Find("tanya3");
        tanya[3] = GameObject.Find("tanya4");
        tanya[4] = GameObject.Find("tanya5");
        sedangMenarik = false;
        for (int i = 0; i < ansCount; i++)
        {
            initAns[i].transform.position = ansPos[i];
            ans[i].transform.position = ansPos[i];
            ans[i].GetComponent<Sistem>().startMarker = initAns[i].transform;
            //Debug.Log(ansPos[i]);
        }


        int num = Random.Range(soal, 4);
        selAns = ans[num];
        selTanya = tanya[0];
        //tanya[soal].GetComponent<ColliderChecker>().jawaban = ans[soal];
        //controlPosInit = controller.transform.position;
    }

    private void Awake()
    {
        
    }

    void Shuffle(Vector3[] deck)
    {
        for (int i = 0; i < deck.Length; i++)
        {
            Vector3 temp = deck[i];
            int randomIndex = Random.Range(0, deck.Length);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    void cekAns()
    {
        if (tanya[soal].GetComponent<ColliderChecker>().collided)
        {

            Debug.Log("ans dan Tanda Bersentuhan!");
            Vector3 posSoal = tanya[soal].transform.position;
            Destroy(tanya[soal]);
            ans[soal].transform.position = posSoal;
            ans[soal].transform.rotation = Quaternion.Euler(0, 0, 0);
            ans[soal].GetComponent<CapsuleCollider2D>().enabled = false;
            ans[soal].GetComponent<Rigidbody2D>().simulated = false;

            soal++;

            //reset posisi katapel
            controller.transform.position = controlPosInit;

            int num = Random.Range(soal, 4);
            selAns = ans[0];

            /*
            string namaPicSpr = ans[soal].GetComponent<SpriteRenderer>().sprite.name;
            Sprite gantiSprite = Resources.Load<Sprite>(namaPicSpr);
            Debug.Log(namaPicSpr);
            tanya[soal].GetComponent<SpriteRenderer>().sprite = gantiSprite;
            tanya[soal].GetComponent<ColliderChecker>().collided = false;
            */
        }
    }

    void makeAns()
    {
        Debug.Log("SedangMenarik = " + sedangMenarik);
        if (!sedangMenarik)
        {
            if (Input.GetMouseButton(0))
            {
                //selAns.GetComponent<Sistem>().selected = false;
                touchPosInit = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(touchPosInit.x, touchPosInit.y);
                raycastInfo = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
                //Debug.Log(raycastInfo.transform.gameObject.GetComponent<Sistem>().isHold);
                if (raycastInfo.collider != null)
                {
                    //Debug.Log("t");
                    if (true)
                    {
                        //Vector3 diatasnyaSling = new Vector3( 0, 1, 0 );
                        selAns = raycastInfo.transform.gameObject;
                        //selAns.transform.position = GameObject.Find("sling").transform.position + diatasnyaSling;
                        //selAns.GetComponent<SpringJoint2D>().enabled = true;
                        if (selAns != ans[soal])
                        {
                            tanya[soal].GetComponent<CapsuleCollider2D>().isTrigger = false;
                        }
                        else
                        {
                            tanya[soal].GetComponent<CapsuleCollider2D>().isTrigger = true;
                        }
                        Debug.Log("[" + selAns.transform.name + "] telah disentuh!");

                        //selAns.GetComponent<Sistem>().selected = true;
                    }

                }
            }
            //if(controller.)
            //selAns.transform.position = controller.transform.position;
        }


    }
    // Update is called once per frame
    void Update()
    {
        //if (tanya[soal].GetComponent<)
        //selTanya.GetComponent<ColliderChecker>().jawaban = ans[soal].gameObject;
        //Debug.Log(tanya[soal]);
        //Debug.Log(ans[soal]);

        if(!sedangMenarik)
        {
            makeAns();
            cekAns();
        }
        
    }
}
