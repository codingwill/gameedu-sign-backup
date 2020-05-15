using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CLASS MEKANIKA FISIS DARI OBJEK LEMPAR
public class Sistem : MonoBehaviour
{
    //DEKLARASI VARIABEL UNTUK KALKULASI FISIKA MEKANISME PELEMPARAN OBJEK
    public bool selected;
    GameObject sistemUtama;
    int soal;
    GameObject[] tandaTanya;
    GameObject[] jawaban;
    //Vector3 touchPosInit;
    GameObject selObj;
    public bool isHold;
    //RaycastHit2D raycastInfo;
    float waktuLepas;
    LineRenderer garis;
    Rigidbody2D rbJawab, sling;
    SpringJoint2D sjJawab;
    public Transform startMarker;
    public Transform endMarker;
    bool thrown;
    Camera cam;
    Transform tfJawab;
    CapsuleCollider2D cldJawab;
    public float maxTarik = 1f;
    bool done = false;
    ScoreCounter scoring;
    Global sistemScript;
    SpriteRenderer srJawab;
    public string kode;
    public AudioClip clue;
    AudioSource slingFx;

    //LERP
    public float speed;
    private float startTime;
    private float journeyLength;

    private Transform diagram;
    
    //INISIALISASI VARIABEL OBJEK LEMPAR BESERTA KOMPONENNYA KETIKA DALAM STATE "AWAKE"
    void Awake()
    {
        rbJawab = GetComponent<Rigidbody2D>();
        sjJawab = GetComponent<SpringJoint2D>();
        garis = GetComponent<LineRenderer>();
        cldJawab = GetComponent<CapsuleCollider2D>();
        sling = GameObject.Find("sling").GetComponent<Rigidbody2D>();
        selected = false;
    }

    //KALKULASI YANG TERUS DIEKSEKUSI SETIAP FRAME SEBANYAK 60 KALI/DETIK SELAMA GAME AKTIF
    void Update()
    {
        if (!done)
        {
            if (sistemUtama.GetComponent<Global>().selAns == transform.gameObject)
            {
                /*
                if (transform.position != startMarker.position)
                {
                    sistemScript.canChoose = false;
                }
                else
                {
                    sistemScript.canChoose = true;
                }
                /*
                if (this.transform.position.y > sling.transform.position.y)
                {
                    selected = true;
                }
                */
                //Debug.Log("isHold = " + isHold);
                //if (Input.touchCount > 0) print(Input.touchCount + " ");

                //Debug.Log(this.gameObject);

                Tarik();
                if (!selected)
                {
                    LerpMaju();
                }

                if (Mathf.Abs(transform.position.y - endMarker.position.y) <= 1e-3)
                {
                    this.GetComponent<SpringJoint2D>().enabled = true;
                    selected = true;
                }
                if (selected && !isHold && thrown)
                {
                    rbJawab.bodyType = RigidbodyType2D.Dynamic;
                    //LerpMundur();
                }
                resetPos();
            }
            else
            {
                selected = false;
                LerpMundur();
            }

            if (selected)
            {
                //if (!thrown) sjJawab.enabled = true;
                //this.GetComponent<Animator>().enabled = false;
            }
            else
            {
                rbJawab.bodyType = RigidbodyType2D.Static;
                //this.GetComponent<Animator>().enabled = true;
            }

            /*
            if (selected)
            {
                GetComponent<CapsuleCollider2D>().isTrigger = false;
            }
            else
            {
                GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
            */
            hideJawaban();
        }
        
    }

    //INISIALISASI VARIABEL OBJEK LEMPAR BESERTA KOMPONENNYA KETIKA PROGRAM BARU DIJALANKAN
    void Start()
    {
        slingFx = GameObject.Find("SlingSound").GetComponent<AudioSource>();
        srJawab = GetComponent<SpriteRenderer>();
        //sjJawab.frequency = 3f;
        scoring = GameObject.Find("Score").GetComponent<ScoreCounter>();
        diagram = GameObject.Find("Samping").GetComponent<Transform>();
        cldJawab.isTrigger = true;
        rbJawab.freezeRotation = true;
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        thrown = false;
        tandaTanya = new GameObject[5];
        jawaban = new GameObject[5];
        sistemUtama = GameObject.Find("ScriptUtama");
        sistemScript = sistemUtama.GetComponent<Global>();
        rbJawab.bodyType = RigidbodyType2D.Static;
        //LERP
        //startMarker = GetComponent<Transform>();
        endMarker = sling.transform;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        speed = 0.1f;
        tfJawab = this.GetComponent<Transform>();

        /*
        soal = 0; //soal pertama dimulai dengan angka 0
        jawaban[0] = GameObject.Find("jawaban1");
        jawaban[1] = GameObject.Find("jawaban2");
        tandaTanya[0] = GameObject.Find("tandaTanya1");
        tandaTanya[1] = GameObject.Find("tandaTanya2");
        */
    }

    //FUNGSI UNTUNG MENGHITUNG JARAK ANTARA KATAPEL DAN POSISI OBJEK
    void SetGarisPos()
    {
        Vector3[] pos = new Vector3[2];
        pos[0] = rbJawab.position;
        pos[1] = sling.position;
        garis.SetPositions(pos);
    }

    //FUNGSI UNTUK MELAKUKAN SIMULASI FISIS PENARIKAN KATAPEL
    void Tarik()
    {
        if (sistemScript.controlEnabled)
        {
            if (isHold)
            {
                sjJawab.enabled = true;
                SetGarisPos();
                garis.enabled = true;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (batasTarik(mousePos))
                {
                    Vector2 arah = (mousePos - sling.position).normalized;
                    rbJawab.position = sling.position + (arah * maxTarik);
                }
                else
                {
                    rbJawab.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                waktuLepas = 1 / (GetComponent<SpringJoint2D>().frequency * 4);
                /*
                touchPosInit = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(touchPosInit.x, touchPosInit.y);
                raycastInfo = Physics2D.Raycast(touchPos, Camera.main.transform.forward);

                if (raycastInfo.collider != null)
                {
                    selObj = raycastInfo.transform.gameObject;
                    if (selObj != jawaban[soal])
                    {
                        tandaTanya[soal].GetComponent<CapsuleCollider2D>().isTrigger = false;
                    }
                    else
                    {
                        tandaTanya[soal].GetComponent<CapsuleCollider2D>().isTrigger = true;
                    }
                    Debug.Log("[" + selObj.transform.name + "] telah disentuh!");
                }
                */

            }
        }

    }
    
    
    void hideJawaban()
    {
        if (!sistemScript.controlEnabled)
        {
            srJawab.enabled = false;
        }
        else
        {
            srJawab.enabled = true;
        }
    }

    //FUNGSI AGAR OBJEK YANG DIPILIH MENGIKUTI POSISI SENTUHAN JARI/MOUSE
    private void OnMouseDown()
    {
        //Debug.Log("Selected = " + selected);
        //Debug.Log("Startmarker = " + startMarker);
        
        if (selected && !sistemScript.isReading)
        {
            isHold = true;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            GameObject.Find("ScriptUtama").GetComponent<Global>().sedangMenarik = true;
        }
    }

    //FUNGSI AGAR OBJEK YANG DIPILIH DAPAT TERLEPAS DARI SLING/KATAPEL
    private void OnMouseUp()
    {
        if(selected && !sistemScript.isReading)
        {
            isHold = false;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            playSling();
            StartCoroutine(Lepas());
            garis.enabled = false;
            
        }
        
    }
    // Update is called once per frame


    void playSling()
    {
        slingFx.Play();
    }
        
    //FUNGSI UNTUK MENGUBAH STATE OBJEK MENJADI "DILEMPAR"
    private IEnumerator Lepas()
    {
        yield return new WaitForSeconds(waktuLepas);
        this.gameObject.GetComponent<SpringJoint2D>().enabled = false;
        thrown = true;
    }

    //LinearInterpolation UNTUK MELAKUKAN PERGERAKAN DARI AREA JAWABAN KE AREA KATAPEL SECARA GARIS LURUS
    void LerpMaju()
    {
        if (!thrown)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, endMarker.position, speed);
        }
    }

    //LinearInterpolation UNTUK MELAKUKAN PERGERAKAN DARI AREA KATAPEL KE AREA JAWABAN SECARA GARIS LURUS
    void LerpMundur()
    {
        if (!thrown)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, startMarker.position, speed);

        }
    }
    
    //FUNGSI UNTUK MERESET POSISI OBJEK JAWABAN YANG TERLEMPAR KE AREA DI LUAR LAYAR MENJADI KE AREA SLING/KATAPEL
    void resetPos()
    {
        
        Vector3 bawKir = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 atKan = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        //Debug.Log("atKan.x = " + atKan.x);
        //Debug.Log("atKan.y = " + atKan.y);
        //Debug.Log("bawKir.x = " + bawKir.x);
        //Debug.Log("bawKir.y = " + bawKir.y);

        if (tfJawab.position.y < bawKir.y)
        {
            //scoring.melencengScore();
            rbJawab.bodyType = RigidbodyType2D.Kinematic;
            rbJawab.velocity = new Vector2(0, 0);
            sjJawab.enabled = false;
            tfJawab.position = endMarker.position + new Vector3(0, 0.01f, 0);
            //sjJawab.enabled = true;
            thrown = false;
            LerpMaju();
            GameObject.Find("ScriptUtama").GetComponent<Global>().sedangMenarik = false;
            sistemScript.melesetSound();
        }
        
    }

    //FUNGSI UNTUK MENENTUKAN BATAS MAKSIMAL JARAK TARIKAN KATAPEL DAN OBJEK
    bool batasTarik(Vector2 mpos)
    {
        //phytagoras
        float jarak = Vector2.Distance(mpos, sling.position);
        return jarak > maxTarik;
    }

    //FUNGSI AGAR POSISI OBJEK JAWABAN YANG MENYENTUH OBJEK TANDA TANYA DISET MENJADI SAMA DENGAN OBJEK TANDA TANYA
    public void correctAns(Vector3 pos)
    {
        done = true;
        //GetComponent<Animator>().enabled = true;
        rbJawab.bodyType = RigidbodyType2D.Static;
        transform.position = pos - new Vector3(0, 0.2f, 0);
        transform.SetParent(diagram, true);
    }
}
