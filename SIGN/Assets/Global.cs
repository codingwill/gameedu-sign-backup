using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Global : MonoBehaviour
{
    // DEKLARASI VARIABEL DALAM CLASS GLOBAL YANG BERFUNGSI SEBAGAI PENYIMPAN INFORMASI DRIVER/PENGENDALI UTAMA GIM //
    public GameObject selAns, selTanya;
    GameObject[] ans, tanya, initAns;
    GameObject controller, correctAns;
    public int soal = 0, ansCount, tanyaCount;
    Vector3 touchPosInit;
    RaycastHit2D raycastInfo;
    Vector3 controlPosInit;
    Vector3[] ansPos;
    public float jarakAnsY, jarakAnsX;
    public bool sedangMenarik;
    ScoreCounter scoring;
    AudioSource soundFx;
    public AudioClip benarFx, benarVc, melesetFx, salahFx, salahVc, audioSoal, audioHint, winFx, slingSound;
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
    public float geserRangeMin, geserRangeMax;
    Camera cam;
    public float geserXPersen;
    int timeScore = 0;
    public int soalAkhir;
    VarLevel varLevel;
    public float zoomIn = 4.491813f, zoomOut = 6.600274f;
    public float tungguOpsi;
    
    void awake()
    {

    }
    // INISIALISASI VARIABEL DALAM CLASS GLOBAL, TERDIRI ATAS INISIALISASI OBJEK KAMERA, TIMEFRAME, LEVEL, SCORE COUNTER, 
    // SERTA [SELURUH] OBJEK-OBJEK LAIN YANG TERDAPAT DI DALAM GAME.
    void Start()
    {
        Time.timeScale = 1;
        tungguOpsi = 0.5f;
        waktuStart = Time.deltaTime;
        slingSound = Resources.Load<AudioClip>("sling");
        varLevel = GameObject.Find("VariabelLevel").GetComponent<VarLevel>();
        sceneSoalSaatIni = GameObject.Find("MainCamera").transform.position;
        camScene = GameObject.FindWithTag("MainCamera");
        cam = camScene.GetComponent<Camera>();
        soundFx = GetComponent<AudioSource>();
        scoring = GameObject.Find("Score").GetComponent<ScoreCounter>();
        ansPos = new Vector3[ansCount];
        for (int i = 0; i < ansCount; i++)
        {
            if (i >= 5)
            {
                ansPos[i] = new Vector3(jarakAnsX, (i % 5 * jarakAnsY) - 2.2f, 0f);
                continue;
            }
            ansPos[i] = new Vector3(0f, (i * jarakAnsY) - 2.2f, 0f);
        }
        Shuffle(ansPos);
        ans = new GameObject[ansCount];
        initAns = new GameObject[ansCount];
        tanya = new GameObject[tanyaCount];
        controller = GameObject.Find("controlJawab");
        soal = 0; //soal pertama dimulai dengan angka 0
        for (int i = 0; i < ansCount; i++)
        {
            string obj = "ans" + (i + 1);
            Debug.Log(obj);
            ans[i] = GameObject.Find(obj);
            obj = "initPosAns" + (i + 1);
            initAns[i] = GameObject.Find(obj);
            if (i < tanyaCount)
            {
                obj = "tanya" + (i + 1);
                tanya[i] = GameObject.Find(obj);
            }

        }
        /*
        ans[0] = GameObject.Find("ans1");
        ans[1] = GameObject.Find("ans2");
        ans[2] = GameObject.Find("ans3");
        ans[3] = GameObject.Find("ans4");
        ans[4] = GameObject.Find("ans5");
        */
        sedangMenarik = false;
        for (int i = 0; i < ansCount; i++)
        {
            initAns[i].transform.localPosition = ansPos[i];
            ans[i].transform.localPosition = ansPos[i];
            ans[i].GetComponent<Sistem>().startMarker = initAns[i].transform;
            //Debug.Log(ansPos[i]);
        }


        int num = Random.Range(soal, ansCount - 1);
        selAns = ans[num];
        selTanya = tanya[0];
        //tanya[soal].GetComponent<ColliderChecker>().jawaban = ans[soal];
        tanya[soal].GetComponent<SpriteRenderer>().color = new Color(242f, 255f, 0, 255f);
        tanya[soal].GetComponent<AnimModif>().animate();
        tanya[soal].GetComponent<AudioSource>().clip = tanya[soal].GetComponent<pembacaSoal>().audioSoal;
        tanya[soal].GetComponent<AudioSource>().Play();
        //controlPosInit = controller.transform.position;
    }

    //BAGIAN KODE YANG AKAN TERUS DIEKSEKUSI PADA SAAT GAME DIJALANKAN. EKSEKUSI DILAKUKAN SEBANYAK 60 KALI DALAM 1 DETIK.
    //KODE BERISI RULES UTAMA DALAM GAME SEPERTI PENGEKSEKUSIAN SOAL SECARA BERURUTAN, MENCOCOKAN JAWABAN DENGAN PERTANYAAN,
    //SERTA SELURUH MEKANIK UTAMA DALAM GAMEPLAY.
    void Update()
    {
        //JIKA INDEKS SOAL LEBIH KECIL DARI JUMLAH SOAL DALAM SUATU LEVEL, MAKA GAME AKAN DIEKSEKUSI DAN BERADA DALAM STATE "PLAYING"
        //JIKA TIDAK, MAKA GAME TIDAK AKAN DIEKSEKUSI, TIMESCALE AKAN DIASSIGN DENGAN 0 YANG ARTINYA MEKANIKANYA TIDAK AKAN BERJALAN
        //LALU GAME AKAN MASUK KE DALAM STATE "GAME OVER"
        if (soal < tanyaCount)
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
                if (timeCounter <= 0)
                    timeCounter = 0;
                else
                    timeCounter -= Time.deltaTime;

                if (controlEnabled)
                {

                    if (!sedangMenarik)
                    {
                        if (canChoose)
                        {
                            camScene.transform.position = Vector3.Lerp(camScene.transform.position, sceneSoalSaatIni, 0.1f);
                            makeAns();

                        }
                    }

                    cekAns();
                }
                //if (tanya[soal].GetComponent<)
                //Debug.Log(Time.time);
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

    public void stopBaca()
    {
        if (tanya[soal].GetComponent<AudioSource>().isPlaying)
        {
            tanya[soal].GetComponent<AudioSource>().Stop();
            isReading = false;
        }
    }

    //FUNGSI UNTUK MEMBACAKAN CLUE/PETUNJUK (TETAPI TIDAK JADI DIPAKAI DI GAME)
    public void clue()
    {
        if (!soundFx.isPlaying)
        {
            soundFx.PlayOneShot(tanya[soal].GetComponent<pembacaSoal>().audioHint);
        }


    }

    //FUNGSI UNTUK MELAKUKAN DELAY SEBELUM USER DAPAT MENGGUNAKAN KATAPEL
    private IEnumerator tungguPilih(float waktu)
    {
        yield return new WaitForSeconds(waktu);
        canChoose = true;
    }

    //FUNGSI UNTUK MELIHAT BAGAN ALUR SECARA KESELURUHAN
    public void lihatBagan()
    {
        if (controlEnabled)
        {
            GameObject.Find("Panel").GetComponent<Image>().enabled = false;
            camScene.transform.position = GameObject.Find("TitikTengah").transform.position;
            camScene.GetComponent<Camera>().orthographicSize = zoomOut;
            controlEnabled = false;
        }
        else
        {
            GameObject.Find("Panel").GetComponent<Image>().enabled = true;
            camScene.transform.position = sceneSoalSaatIni;
            camScene.GetComponent<Camera>().orthographicSize = zoomIn;
            controlEnabled = true;
        }

    }

    private void Awake()
    {

    }

    //FUNGSI UNTUK MELAKUKAN RANDOMIZATION URUTAN OPSI JAWABAN PADA GAME 
    //SEHINGGA SETIAP KALI GAME DIEKSEKUSI, URUTAN OPSI AKAN SELALU BERBEDA
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

    //FUNGSI UNTUK MEMAINKAN EFEK SUARA MELEMPAR JAWABAN DENGAN KATAPEL
    public void playSound()
    {
        soundFx.PlayOneShot(slingSound);
    }

    //FUNGSI UNTUK MEMERIKSA JAWABAN YANG BERTABRAKAN DENGAN IKON TANDA TANYA
    void cekAns()
    {
        if (soal < ansCount)
        {
            if (tanya[soal].GetComponent<ColliderChecker>().collided)
            {
                //APABILA BENAR, MAKA SELURUH KODE INI AKAN DIEKSEKUSI
                //OBJEK JAWABAN DAN TANDA TANYAKAN DIHAPUS
                //EFEK SUARA BENAR AKAN DIMAINKAN
                if (tanya[soal].GetComponent<ColliderChecker>().correct)
                {
                    Debug.Log("TES");
                    soundFx.PlayOneShot(benarFx);
                    soundFx.Play();
                    scoring.addScore();
                    //Debug.Log("ans dan Tanda Bersentuhan!");
                    Vector3 posSoal = tanya[soal].transform.position;
                    Destroy(tanya[soal]);
                    
                    correctAns = tanya[soal].GetComponent<ColliderChecker>().collidedObject;
                    Destroy(correctAns);
                    /*
                    correctAns.transform.rotation = Quaternion.Euler(0, 0, 0);
                    correctAns.GetComponent<CapsuleCollider2D>().enabled = false;
                    correctAns.GetComponent<Rigidbody2D>().simulated = false;
                    correctAns.GetComponent<Sistem>().correctAns(posSoal);
                    
                    tanya[soal].GetComponent<SpriteRenderer>().color = new Color(242f, 255f, 0, 255f);
                    */
                    //reset posisi katapel
                    //controller.transform.position = controlPosInit;
                    soal++;
                    int num = Random.Range(soal, 4);
                    //selAns = ans[0];
                    sedangMenarik = false;

                    geserKameraKeSoal();
                    tanya[soal].GetComponent<AnimModif>().animate();
                    StartCoroutine(playSoal(2f));
                    
                    


                }
                //APABILA SALAH, HANYA EFEK SUARA SALAH YANG AKAN DIEKSEKUSI
                else
                {
                    if (!soundPlayed)
                    {
                        soundPlayed = true;
                        soundFx.PlayOneShot(salahVc);
                        scoring.salahScore();
                    }

                }

            }
        }
        
    }
    
    //IEnumerator disable

    //FUNGSI PEMBACA SOAL AGAR DIBERIKAN DELAY SETELAH SOAL SEBELUMNYA SELESAI
    IEnumerator playSoal(float waktu)
    {
        yield return new WaitForSeconds(waktu);
        tanya[soal].GetComponent<AudioSource>().Play();
    }


    //FUNGSI KETIKA MEMASUKI STATE "GAME OVER"
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
        GameObject.Find("NextButton").GetComponent<Image>().enabled = true;
        /*
        PlayerPrefs.SetInt("TimeScore2", (int)timeCounter * 2);
        PlayerPrefs.SetInt("FinishScore2", scoring.score);
        */
        varLevel.addTimeScore((int)timeCounter * 2);
        varLevel.addFinishScore(scoring.score);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        PlayerPrefs.Save();
        Time.timeScale = 0;
        
        
        
    }

    //FUNGSI UNTUK MENAMBAHKAN SCORE WAKTU YANG BISA DIAKSES OLEH CLASS LAIN
    public void addTimeScore(int score)
    {
        timeScore = score;
    }

    //FUNGSI UNTUK RESTART LEVEL
    public void restartLevel()
    {
        int level = PlayerPrefs.GetInt("Level");
        if (level == 1) SceneManager.LoadScene("AnaLevel1");
        else if (level == 2) SceneManager.LoadScene("AnaLevel2");
        else if (level == 3) SceneManager.LoadScene("KataLevel1");
        else if (level == 4) SceneManager.LoadScene("KataLevel2");
        else if (level == 5) SceneManager.LoadScene("KataLevel3");
        else if (level == 6) SceneManager.LoadScene("KataLevel4");
        else if (level == 7) SceneManager.LoadScene("KataLevel5");
    }

    //FUNGSI UNTUK GESER KAMERA KETIKA SOAL SOLVED
    public void geserKameraKeSoal()
    {
        Vector3 bawKir = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 atKan = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        sceneSoalSaatIni = camScene.transform.position;
        sceneSoalSaatIni.y = tanya[soal].transform.position.y - Random.Range(geserRangeMin, geserRangeMax);
        sceneSoalSaatIni.x = tanya[soal].transform.position.x - (atKan.x - bawKir.x) * geserXPersen;
        //camScene.transform.position = sceneSoalSaatIni;
        
    }

    //FUNGSI KETIKA OBJEK LEMPAR KELUAR AREA LAYAR
    public void melesetSound()
    {
        soundFx.PlayOneShot(melesetFx);
    }

    //FUNGSI UNTUK MENGGESER OBJEK YANG DIPILIH MENUJU KE AREA KATAPEL
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
                        StartCoroutine(tungguPilih(tungguOpsi));
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