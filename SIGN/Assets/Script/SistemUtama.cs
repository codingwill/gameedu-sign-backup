using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemUtama : MonoBehaviour
{
    int soal;
    GameObject[] tandaTanya;
    GameObject[] jawaban;
    Vector3 touchPosInit;
    GameObject selObj;
    // Start is called before the first frame update
    void Start()
    {
        tandaTanya = new GameObject[5];
        jawaban = new GameObject[5];
        soal = 0; //soal pertama dimulai dengan angka 0
        jawaban[0] = GameObject.Find("jawaban1");
        jawaban[1] = GameObject.Find("jawaban2");
        tandaTanya[0] = GameObject.Find("tandaTanya1");
        tandaTanya[1] = GameObject.Find("tandaTanya2");
    }

    // Update is called once per frame
    void Update()
    {

        //if (tandaTanya[soal].GetComponent<)
        tandaTanya[soal].GetComponent<ColliderChecker>().jawaban = jawaban[soal].gameObject;
        if (tandaTanya[soal].GetComponent<ColliderChecker>().collided)
        {
            Debug.Log("Jawaban dan Tanda Bersentuhan!");
            Vector3 posSoal = tandaTanya[soal].transform.position;
            Destroy(tandaTanya[soal]);
            jawaban[soal].transform.position = posSoal;
            jawaban[soal].transform.rotation = Quaternion.Euler(0, 0, 0);
            jawaban[soal].GetComponent<CapsuleCollider2D>().enabled = false;
            jawaban[soal].GetComponent<Rigidbody2D>().simulated = false;

            soal++;
            /*
            string namaPicSpr = jawaban[soal].GetComponent<SpriteRenderer>().sprite.name;
            Sprite gantiSprite = Resources.Load<Sprite>(namaPicSpr);
            Debug.Log(namaPicSpr);
            tandaTanya[soal].GetComponent<SpriteRenderer>().sprite = gantiSprite;
            tandaTanya[soal].GetComponent<ColliderChecker>().collided = false;
            */
        }

        
        //if (Input.touchCount > 0) print(Input.touchCount + " ");
        if (Input.GetMouseButton(0))
        {
            touchPosInit = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(touchPosInit.x, touchPosInit.y);
            RaycastHit2D raycastInfo = Physics2D.Raycast(touchPos, Camera.main.transform.forward);

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
                selObj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                selObj.GetComponent<Rigidbody2D>().gravityScale = 0;
                //Debug.Log("[" + selObj.transform.name + "] telah disentuh!");
                selObj.transform.position = touchPos;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            selObj.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            


        }

    }
}
