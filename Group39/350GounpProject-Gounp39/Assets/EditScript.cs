using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;

public class EditScript : MonoBehaviour
{
    public string editusername;
    public string editphone;
    public string editemail;
    public string find = "find";
    public GameObject Loading;
    public GameObject Finish;
    public GameObject wrongenametext;
    public GameObject wrongephonetext;
    public GameObject wrongeemailtext;
    bool checkphone;
    bool checkname;
    bool checkemail;


    public LoginScript loginUser;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(login.myLoginUserdatalist.LD[0].loginuser);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void usernameInputvaid(string un)
    {
        editusername = un;
        //Debug.Log(editusername);
    }
    public void phoneInputvaid(string up)
    {
        editphone = up;
        //Debug.Log(editphone);
    }
    public void emailInputvaid(string ue)
    {
        editemail = ue;
        //Debug.Log(editemail);
    }
    public void UserProfileEdit()
    {
        checkphone = Regex.IsMatch(editphone, @"[0-9]{8}$");
        checkname = Regex.IsMatch(editusername, @"^[A-Za-z]+$");
        checkemail = Regex.IsMatch(editemail, @"^\w+@\w+.+[a-z]{3}$");
        if (checkphone == true && checkname == true && checkemail == true)
        {
            wrongenametext.SetActive(false);
            wrongephonetext.SetActive(false);
            Loading.SetActive(true);
            StartCoroutine(UPEN());
            StartCoroutine(UPEP());
            StartCoroutine(UPEEM());
            StartCoroutine(FinishS());
        }
        if(checkphone == false)
        {
            wrongephonetext.SetActive(true);
        }
        else
        {
            wrongephonetext.SetActive(false);
        }
        if(checkname == false)
        {
            wrongenametext.SetActive(true) ;
        }
        else
        {
            wrongenametext.SetActive(false) ;
        }
        if (checkemail == false)
        {
            wrongeemailtext.SetActive(true) ;
        }
        else 
        {
            wrongeemailtext.SetActive(false);
        }
    }
    IEnumerator UPEN()
    {
        WWWForm form = new WWWForm();
        int Row = 2;
        form.AddField("method", "Edit");
        form.AddField("userid", loginUser.useridinput);
        form.AddField("EditData", editusername);
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
                Debug.Log("Name upload complete!");
            }
        }
    }
    IEnumerator UPEP()
    {
        WWWForm form = new WWWForm();
        int Row = 3;
        form.AddField("method", "Edit");
        form.AddField("userid", loginUser.useridinput);
        form.AddField("EditData", editphone);
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
                Debug.Log("Phone upload complete!");
            }
        }
    }
    IEnumerator UPEEM()
    {
        WWWForm form = new WWWForm();
        int Row = 4;
        form.AddField("method", "Edit");
        form.AddField("userid", loginUser.useridinput);
        form.AddField("EditData", editemail);
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
                Debug.Log("Email upload complete!");
            }
        }
    }
    IEnumerator FinishS()
    {
        yield return new WaitForSeconds(1.5f);
        Finish.SetActive(true);
    }
}
