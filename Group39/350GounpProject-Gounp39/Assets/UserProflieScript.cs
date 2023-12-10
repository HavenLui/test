using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static StudentTable;

public class UserProflieScript : MonoBehaviour
{
    public int C = 2;
    public int R;
    public TextMeshProUGUI UN;
    public TextMeshProUGUI UP;
    public TextMeshProUGUI UEM;

    public LoginScript loginUser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadUserProflieData()
    {
        StartCoroutine(RUPDN());
        StartCoroutine(RUPDP());
        StartCoroutine(RUPDEM());
    }
    IEnumerator RUPDN()
    {
        WWWForm form = new WWWForm();
        int Row = 2;
        form.AddField("method", "ReadUserData");
        form.AddField("userid", loginUser.useridinput);
        form.AddField("Row", Row);
        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                UN.text = www.downloadHandler.text;
            }
        }
    }
    IEnumerator RUPDP()
    {
        WWWForm form = new WWWForm();
        int Row = 3;
        form.AddField("method", "ReadUserData");
        form.AddField("userid", loginUser.useridinput);
        form.AddField("Row", Row);
        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                UP.text = www.downloadHandler.text;
            }
        }
    }
    IEnumerator RUPDEM()
    {
        WWWForm form = new WWWForm();
        int Row = 4;
        form.AddField("method", "ReadUserData");
        form.AddField("userid", loginUser.useridinput);
        form.AddField("Row", Row);
        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                UEM.text = www.downloadHandler.text;
            }
        }
    }
}
