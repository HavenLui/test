using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    public string useridinput;
    public string passwordinput;
    public string Permissions;
    public int C = 1;
    public int R = 1;
    string UserLoginNowfilename = "";
    public GameObject WUID;
    public GameObject WPW;
    public UserProflieScript UPS;
    public GameObject mainmeun;
    public GameObject System;


    // Start is called before the first frame update
    void Start()
    {
        UserLoginNowfilename = Application.dataPath + "/LoginUser.csv";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void useridget(string uid)
    {
        useridinput = uid;
    }
    public void passwordget(string pw)
    {
        passwordinput = pw;
    }
    public void login()
    {
        StartCoroutine(L());
        StartCoroutine(p());
    }
    IEnumerator L()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "Unlog");
        form.AddField("UID", useridinput);
        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if(passwordinput == www.downloadHandler.text)
                {
                    Debug.Log("Login Success");
                    WPW.SetActive(false);
                    WUID.SetActive(false);
                    mainmeun.SetActive(true);
                    System.SetActive(true);
                }
                else if (www.downloadHandler.text == "")
                {
                    WPW.SetActive(false);
                    WUID.SetActive(true);
                }
                else if(passwordinput != www.downloadHandler.text)
                {
                    WPW.SetActive(true);
                    WUID.SetActive(false);
                }
            }
        }
    }
    IEnumerator p()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "GetPermissions");
        form.AddField("UID", useridinput);
        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Permissions = www.downloadHandler.text;
            }
        }
    }
    public void LoadLoginUserData()
    {
        //StartCoroutine(LLUD());
        TextWriter tw = new StreamWriter(UserLoginNowfilename, false);
        tw.WriteLine("LoninUserID, Password");
        tw.Close();

        tw = new StreamWriter(UserLoginNowfilename, true);
        tw.WriteLine(useridinput+","+ passwordinput);
        tw.Close();
        UserLoginNowfilename = Application.dataPath + "/LoginUser.csv";

    }
}
