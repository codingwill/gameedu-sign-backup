using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Global2 : MonoBehaviour
{
    public GameObject selAns, selTanya;
    GameObject[] ans, tanya, initAns;
    GameObject controller, correctAns;
    public int soal = 0, ansCount = 7;
    Vector3 touchPosInit;
    RaycastHit2D raycastInfo;
    Vector3 controlPosInit;
    Vector3[] ansPos;
    float jarakAns = 0.7f;
    public bool sedangMenarik;
    ScoreCounter scoring;
    AudioSource soundFx;
    public AudioClip benarFx, benarVc, melesetFx, salahFx, salahVc, audioSoal, audioHint, winFx;
    private SpriteRenderer spTanya;
    public bool soundPlayed = false;
    GameObject camScene;
    public bool controlEnabled = true;
    Vector3 sceneSoalSaatIni;
    public bool canChoose = true;
    public int skor;
    public float waktuStart, timeCounter;
    public bool isReading;
    bool winSound = false;
    void awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        waktuStart = Time.deltaTime;
        sceneSoalSaatIni = GameObject.FindWithTag("MainCamera").transform.position;
        camScene = GameObject.FindWithTag("MainCamera");
        soundFx = GetComponent<AudioSource>();
        scoring = GameObject.Find("Score").GetComponent<ScoreCounter>();
        ansPos = new Vector3[ansCount];
        for (int i = 0; i < ansCount; i++)
        {
            ansPos[i] = new Vector3(0f, (i * jarakAns) - 2f, 0f);
        }
        Shuffle(ansPos);
        ans = new GameObject[ansCount];
        initAns = new GameObject[ansCount];
        tanya = new GameObject[ansCount];
        controller = GameObject.Find("controlJawab");
        soal = 0; //soal pertama dimulai dengan angka 0
        ans[0] = GameObject.Find("ans1");
        ans[1] = GameObject.Find("ans2");
        ans[2] = GameObject.Find("ans3");
        ans[3] = GameObject.Find("ans4");
        ans[4] = GameObject.Find("ans5");
        ans[5] = GameObject.Find("ans6");
        ans[6] = GameObject.Find("ans7");
        initAns[0] = GameObject.Find("initPosAns1");
        initAns[1] = GameObject.Find("initPosAns2");
        initAns[2] = GameObject.Find("initPosAns3");
        initAns[3] = GameObject.Find("initPosAns4");
        initAns[4] = GameObject.Find("initPosAns5");
        initAns[5] = GameObject.Find("initPosAns6");
        initAns[6] = GameObject.Find("initPosAns7");
        tanya[0] = GameObject.Find("tanya1");
        tanya[1] = GameObject.Find("tanya2");
        tanya[2] = GameObject.Find("tanya3");
        tanya[3] = GameObject.Find("tanya4");
        tanya[4] = GameObject.Find("tanya5");
        tanya[5] = GameObject.Find("tanya6");
        tanya[6] = GameObject.Find("tanya7");
        sedangMenarik = false;
        for (int i = 0; i < ansCount; i++)
        {
            initAns[i].transform.localPosition = ansPos[i];
            ans[i].transform.localPosition = ansPos[i];
            ans[i].GetComponent<Sistem>().startMarker = initAns[i].transform;
            //Debug.Log(ansPos[i]);
        }

        
        int num = Random.Range(soal, 4);
        selAns = ans[num];
        selTanya = tanya[0];
        //tanya[soal].GetComponent<ColliderChecker>().jawaban = ans[soal];
        tanya[soal].GetComponent<SpriteRenderer>().color = new Color(242f, 255f, 0, 255f);
        tanya[soal].GetComponent<AnimModif>().animate();
        tanya[soal].GetComponent<AudioSource>().clip = tanya[soal].GetComponent<pembacaSoal>().audioSoal;
        tanya[soal].GetComponent<AudioSource>().Play();
        //controlPosInit = controller.transform.position;
    }

    void Update()
    {
        if (soal < ansCount)
        {
            if (tanya[soal].GetComponent<AudioSource>().isPlaying || (soundFx.isPlaying && soundFx.clip == benarVc))
            {
                isReading = true;
            }
            else
            {
                isReading = false;
            }

            if (!isReading)
            {
                timeCounter -= Time.deltaTime;
                if (controlEnabled)
                {
                    camScene.transform.position = Vector3.Lerp(camScene.transform.position, sceneSoalSaatIni, 0.1f);
                    if (!sedangMenarik)
                    {
                        if (canChoose)
                        {
                            makeAns();

                        }
                    }

                    cekAns();
                }
                //if (tanya[soal].GetComponent<)
                Debug.Log(Time.time);
                if (soal < tanya.Length)
                {
                    //tanya[soal].GetComponent<ColliderChecker>().jawaban = ans[soal].gameObject;
                }

                //Debug.Log(tanya[soal]);
                //Debug.Log(ans[soal]);
                /*
                for (int i = 0; i < ansCount; i++)
                {
                    if (ans[i].GetComponent<Sistem>().isHold)
                    {
                        sedangMenarik = true;
                    }
                }
                */
            }
        }
        else
        {
            if (!winSound)
            {

                StartCoroutine(playWin(2.5f));
            }
        }


    }

    private IEnumerator tungguPilih(float waktu)
    {
        yield return new WaitForSeconds(waktu);
        canChoose = true;
    }

    public void lihatBagan()
    {
        if (controlEnabled)
        {
            camScene.transform.position = new Vector3(-0.04f, -1.17f, -10f);
            camScene.GetComponent<Camera>().orthographicSize = 6.600274f;
            controlEnabled = false;
        }
        else
        {
            camScene.transform.position = sceneSoalSaatIni;
            camScene.GetComponent<Camera>().orthographicSize = 4.491813f;
            controlEnabled = true;
        }

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
        if (soal < ansCount)
        {
            if (tanya[soal].GetComponent<ColliderChecker>().collided)
            {
                if (tanya[soal].GetComponent<ColliderChecker>().correct)
                {

                    soundFx.PlayOneShot(benarFx);
                    soundFx.Play();
                    scoring.addScore();
                    //Debug.Log("ans dan Tanda Bersentuhan!");
                    Vector3 posSoal = tanya[soal].transform.position;
                    Destroy(tanya[soal]);
                    correctAns = tanya[soal].GetComponent<ColliderChecker>().collidedObject;
                    correctAns.transform.rotation = Quaternion.Euler(0, 0, 0);
                    correctAns.GetComponent<CapsuleCollider2D>().enabled = false;
                    correctAns.GetComponent<Rigidbody2D>().simulated = false;
                    correctAns.GetComponent<Sistem>().correctAns(posSoal);
                    soal++;
                    tanya[soal].GetComponent<SpriteRenderer>().color = new Color(242f, 255f, 0, 255f);
                    //reset posisi katapel
                    //controller.transform.position = controlPosInit;

                    int num = Random.Range(soal, 4);
                    selAns = ans[0];
                    sedangMenarik = false;

                    geserKameraKeSoal();
                    tanya[soal].GetComponent<AnimModif>().animate();
                    StartCoroutine(playSoal(2f));


                }
                else
                {
                    if (!soundPlayed)
                    {
                        soundPlayed = true;
                        soundFx.PlayOneShot(salahVc);

                    }

                }

            }
        }

    }

    IEnumerator playSoal(float waktu)
    {
        yield return new WaitForSeconds(waktu);
        tanya[soal].GetComponent<AudioSource>().Play();
    }

    IEnumerator playWin(float waktu)
    {

        yield return new WaitForSeconds(waktu);
        GameObject.Find("BGMUtama").GetComponent<AudioSource>().Stop();
        soundFx.PlayOneShot(winFx);
        winSound = true;
        GameObject.Find("PanelBonusTime").GetComponent<Image>().enabled = true;
        GameObject.Find("PanelLvlScore").GetComponent<Image>().enabled = true;
        GameObject.Find("PanelLvlClear").GetComponent<Image>().enabled = true;
        GameObject.Find("TextBonusTime").GetComponent<Text>().enabled = true;
        GameObject.Find("TextLvlScore").GetComponent<Text>().enabled = true;
        GameObject.Find("TextLvlClear").GetComponent<Text>().enabled = true;
        //Time.timeScale = 0;



    }

    public void restartLevel()
    {
        SceneManager.LoadScene("KataLevel1");
    }

    public void geserKameraKeSoal()
    {
        sceneSoalSaatIni = camScene.transform.position;
        sceneSoalSaatIni.y = tanya[soal].transform.position.y - Random.Range(-1f, 2f);
        //camScene.transform.position = sceneSoalSaatIni;

    }

    public void melesetSound()
    {
        soundFx.PlayOneShot(melesetFx);
    }

    void makeAns()
    {
        //Debug.Log("SedangMenarik = " + sedangMenarik);
        if (!sedangMenarik)
        {
            if (Input.GetMouseButton(0))
            {
                //selAns.GetComponent<Sistem>().selected = false;
                touchPosInit = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(touchPosInit.x, touchPosInit.y);
                raycastInfo = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
                //Debug.Log(raycastInfo.transform.gameObject.GetComponent<Sistem>().isHold);
                if (raycastInfo.collider != null && raycastInfo.transform.tag != "Tanya")
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
                        canChoose = false;
                        StartCoroutine(tungguPilih(0.3f));
                        //Debug.Log("[" + selAns.transform.name + "] telah disentuh!");

                        //selAns.GetComponent<Sistem>().selected = true;
                    }

                }
            }
            //if(controller.)
            //selAns.transform.position = controller.transform.position;
        }
    }

    void bacaSoal()
    {

    }
    // Update is called once per frame

}
/*
 * 
 * 
        //Debug.Log("SedangMenarik = " + sedangMenarik);
        if (!sedangMenarik)
        {
            //if (Input.GetMouseButton(0))
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    //selAns.GetComponent<Sistem>().selected = false;
                    touchPosInit = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
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
                            //Debug.Log("[" + selAns.transform.name + "] telah disentuh!");

                            //selAns.GetComponent<Sistem>().selected = true;
                        }

                    }
                }
                
            }
            //if(controller.)
            //selAns.transform.position = controller.transform.position;
        }
*/
