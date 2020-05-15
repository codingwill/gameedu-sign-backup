using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;

//CLASS UNTUK MELAKUKAN SAVE IDENTITAS USER DAN INTEGRASI DATABASE DARI WEB KE GAME (VICE VERSA)
public class SaveIdentitas : MonoBehaviour
{
    string playerUsername;
    string playerName;
    string playerSekolah;
    int playerScore = 0;
    int playerTimeScore = 0;
    public GameObject username, nama, sekolah;
    public UsersList userList = new UsersList();
    bool unik = true;
    public string getData;
    string json;
    bool matched = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerUsername = username.GetComponent<InputField>().text;
        playerName = nama.GetComponent<InputField>().text;
        playerSekolah = sekolah.GetComponent<InputField>().text;

        //Debug.Log("playerUsername = " + playerUsername);
    }

    //FUNGSI UNTUK MELAKUKAN PENYIMPANAN DATA USER KE GLOBAL VARIABEL
    public void saveUser()
    {
        PlayerPrefs.SetString("Username", playerUsername);
        PlayerPrefs.SetString("Nama", playerName);
        PlayerPrefs.SetString("Sekolah", playerSekolah);
        PlayerPrefs.SetInt("FinishScore", playerScore);
        PlayerPrefs.SetInt("TimeScore", playerTimeScore);

        if (playerUsername != "" && playerName != "" && playerSekolah != "")
        {
            cekUser();
            if (unik)
            {
                submitUser();
                resetScore();
                startFromZero();
                SceneManager.LoadScene("MenuScene");
            }
            else
            {
                loginUser();
                if (matched)
                {
                    PlayerPrefs.SetString("Username", playerUsername);
                    PlayerPrefs.SetString("Nama", playerName);
                    PlayerPrefs.SetString("Sekolah", playerSekolah);
                    PlayerPrefs.SetInt("FinishScore", playerScore);
                    PlayerPrefs.SetInt("TimeScore", playerTimeScore);
                    startFromZero();
                    SceneManager.LoadScene("MenuScene");
                }
                else
                {
                    username.GetComponent<InputField>().placeholder.GetComponent<Text>().color = new Color(255f, 0, 0, 255f);
                    username.GetComponent<InputField>().text = "";
                    username.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "Username sudah terpakai!";
                }
                
            }
            
        }
        else
        {
            if (playerUsername == "")
            {
                username.GetComponent<InputField>().placeholder.GetComponent<Text>().color = new Color(255f, 0, 0, 255f);
                username.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "Tidak boleh kosong!";
            }
            if (playerName == "")
            {
                nama.GetComponent<InputField>().placeholder.GetComponent<Text>().color = new Color(255f, 0, 0, 255f);
                nama.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "Tidak boleh kosong!";
            }
            if (playerSekolah == "")
            {
                sekolah.GetComponent<InputField>().placeholder.GetComponent<Text>().color = new Color(255f, 0, 0, 255f);
                sekolah.GetComponent<InputField>().placeholder.GetComponent<Text>().text = "Tidak boleh kosong!";
            }

        }
    }

    //FUNGSI UNTUK MERESET LEVEL MENJADI LEVEL 1
    void startFromZero()
    {
        PlayerPrefs.SetInt("Level", 1);
    }

    //FUNGSI UNTUK MELAKUKAN PENCOCOKAN DATA USER DI GAME DENGAN DATA USER DI WEB
    public void cekUser()
    {
        //TextAsset json = Resources.Load("dummy") as TextAsset;

        WWW www = new WWW(getData);
        StartCoroutine(ambilData(www));
        
        if (json != "")
        {
            unik = true;
            userList = JsonUtility.FromJson<UsersList>("{\"user\":" + json + "}");
            foreach (User user in userList.user)
            {
                print(user.username);
                if (user.username == playerUsername)
                {
                    unik = false;
                    break;
                }
            }
        }
        else
        {
            print("Asset is null.");
        }
    }

    //FUNGSI UNTUK MELAKUKAN PENGAMBILAN DATA HINGGA SELESAI
    IEnumerator ambilData(WWW www)
    {
        //StartCoroutine(wait(5));
        yield return www;
        json = www.text;

    }

    IEnumerator wait(float t)
    {
        yield return new WaitForSeconds(t);
    }
    
    //FUNGSI UNTUK MELAKUKAN PENGIRIMAN DATA HINGGA SELESAI
    IEnumerator kirimData(WWW www)
    {
        //StartCoroutine(wait(5));
        yield return www;
    }


    //FUNGSI UNTUK MELAKUKAN PENGIRIMAN DATA USER KE WEB
    void submitUser()
    {

        string url = "https://us-central1-sign-e15cc.cloudfunctions.net/addUser?asalSekolah=" + playerSekolah + "&nama=" + playerName + "&username=" + playerUsername;
        WWW www = new WWW(url);
        StartCoroutine(kirimData(www));
    }

    //FUNGSI UNTUK MELAKUKAN SISTEM LOGIN DENGAN MENCOCOKAN DATA USER YANG ADA DI WEB DENGAN INPUTAN DI GAME
    void loginUser()
    {
        WWW www = new WWW(getData);
        StartCoroutine(ambilData(www));

        if (json != "")
        {
            matched = false;
            userList = JsonUtility.FromJson<UsersList>("{\"user\":" + json + "}");
            foreach (User user in userList.user)
            {
                print(user.username);
                if (user.username == playerUsername)
                {
                    if (user.asalSekolah == playerSekolah && user.nama == playerName)
                    {
                        matched = true;
                        playerTimeScore = System.Convert.ToInt32(user.timeScore);
                        playerScore = System.Convert.ToInt32(user.finishScore);
                    }
                }
            }
        }
        else
        {
            print("Asset is null.");
        }
    }
    
    //FUNGSI UNTUK MELAKUKAN RESET SCORE
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
}
