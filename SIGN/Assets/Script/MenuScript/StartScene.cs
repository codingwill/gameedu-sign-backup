using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {

    // Use this for initialization

    //public AudioClip clickSound;
    AudioSource soundFx;
    void Start() {
        //clickSound.LoadAudioData();
        //clickSound = (AudioClip) Resources.Load("Audio/menuSelect.mp3");
        soundFx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }



    public void loadMenu()
    {
        //soundFx.Play();
        SceneManager.LoadScene("MenuScene");

    }
    public void loadKata()
    {
        //soundFx.Play();

        SceneManager.LoadScene("KataScene");

    }
    public void loadKata1()
    {
        //soundFx.Play();

        SceneManager.LoadScene("KataLevel1");

    }
    public void loadKata2()
    {
        //soundFx.Play();

        SceneManager.LoadScene("KataLevel2");

    }
    public void loadKata3()
    {
        //soundFx.Play();

        SceneManager.LoadScene("KataLevel3");

    }
    public void loadKata4()
    {
        //soundFx.Play();

        SceneManager.LoadScene("KataLevel4");

    }
    public void loadKata5()
    {
        //soundFx.Play();

        SceneManager.LoadScene("KataLevel5");

    }
    public void kataVid()
    {
        //soundFx.Play();

        SceneManager.LoadScene("KataVideo");

    }

    public void anaVid()
    {
        //soundFx.Play();

        SceneManager.LoadScene("AnaVideo");

    }

    public void loadAna()
    {
        //soundFx.Play();

        SceneManager.LoadScene("AnaScene");

    }

    public void loadFirst()
    {
        //soundFx.Play();

        SceneManager.LoadScene("FirstScene");

    }
    public void loadAkhir()
    {
        //soundFx.Play();

        SceneManager.LoadScene("ScoreAkhir");

    }

    public void loadKD()
    {
        //soundFx.Play();

        SceneManager.LoadScene("KD");

    }
    public void loadTujuan()
    {
        //soundFx.Play();

        SceneManager.LoadScene("Tujuan");

    }

    public void loadProfil()
    {
        //soundFx.Play();

        SceneManager.LoadScene("Profil");

    }

    public void loadSoal()
    {
        //soundFx.Play();

        SceneManager.LoadScene("Soal");

    }

    public void loadHelp()
    {
        //soundFx.Play();

        SceneManager.LoadScene("Help");

    }

    public void loadAna1()
    {
        //soundFx.Play();

        SceneManager.LoadScene("AnaLevel1");

    }

    public void loadAna2()
    {
        //soundFx.Play();

        SceneManager.LoadScene("AnaLevel2");

    }

    public void loadCredits()
    {
        //soundFx.Play();

        SceneManager.LoadScene("Credits");

    }

    public void loadIdentitas()
    {
        //soundFx.Play();

        SceneManager.LoadScene("Identitas");

    }

    public void tutorial()
    {
        SceneManager.LoadScene("TutorialVideo");
    }

    public void soal1()
    {
        Application.OpenURL("http://bit.ly/gamesignsoal1");
    }
    public void soal2()
    {
        Application.OpenURL("http://bit.ly/gamesignsoal2");
    }
    public void soal3()
    {
        Application.OpenURL("http://bit.ly/gamesignsoal3");
    }
    public void soal4()
    {
        Application.OpenURL("http://bit.ly/gamesignsoal4");
    }
    public void soal5()
    {
        Application.OpenURL("http://bit.ly/gamesignsoal5");
    }

    public void poster()
    {
        Application.OpenURL("http://bit.ly/postergamesign");
        Application.OpenURL("https://sign-e15cc.firebaseapp.com/detail?username=" + PlayerPrefs.GetString("Username"));
        Application.OpenURL("https://us-central1-sign-e15cc.cloudfunctions.net/server?username=" + PlayerPrefs.GetString("Username"));
    }

    public void byPassLogin()
    {
        resetScore();
        startFromZero();
        loadMenu();

    }

    void resetScore()
    {
        PlayerPrefs.SetInt("TimeScore1", 0);
        PlayerPrefs.SetInt("TimeScore2", 0);
        PlayerPrefs.SetInt("TimeScore3", 0);
        PlayerPrefs.SetInt("TimeScore4", 0);
        PlayerPrefs.SetInt("TimeScore5", 0);
        PlayerPrefs.SetInt("TimeScore6", 0);
        PlayerPrefs.SetInt("TimeScore7", 0);

        PlayerPrefs.SetInt("FinishScore1", 0);
        PlayerPrefs.SetInt("FinishScore2", 0);
        PlayerPrefs.SetInt("FinishScore3", 0);
        PlayerPrefs.SetInt("FinishScore4", 0);
        PlayerPrefs.SetInt("FinishScore5", 0);
        PlayerPrefs.SetInt("FinishScore6", 0);
        PlayerPrefs.SetInt("FinishScore7", 0);
    }

    void startFromZero()
    {
        PlayerPrefs.SetInt("Level", 1);
    }

    public void quit()
    {
        Application.Quit();
    }

	private void OnMouseDown () {
		SceneManager.LoadScene("MenuScene");
	}

}
