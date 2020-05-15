using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonParser : MonoBehaviour
{
    public UsersList userList = new UsersList();
    // Start is called before the first frame update
    void Start()
    {
        TextAsset json = Resources.Load("dummy") as TextAsset;
        if (json != null)
        {
            userList = JsonUtility.FromJson<UsersList>("{\"user\":" + json.text + "}");
            foreach (User user in userList.user)
            {
                print(user.nama);
            }
        }
        else
        {
            print("Asset is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
