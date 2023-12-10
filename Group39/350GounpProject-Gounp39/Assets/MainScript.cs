using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class MainScript : MonoBehaviour
{
    public LoginScript LS;
    public string userid;
    public string username = "";
    public TextMeshProUGUI welcometext;

    public GameObject coursepanel_student;
    public GameObject coursepanel_teacher;

    public GameObject resultpanel_student;
    public GameObject resultpanel_teacher;

    // Start is called before the first frame update
    void Start()
    {
        userid = LS.useridinput;
        welcometext.text = "Welcome back, " + username + "!";
    }

    // Update is called once per frame
    void Update()
    {
        userid = LS.useridinput;
        welcometext.text = "Welcome back, " + username + "!";
    }
    public void FindName()
    {
        StartCoroutine(FN());
    }
    IEnumerator FN()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "FindName");
        form.AddField("userid", userid);

        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log("Form upload complete!");
                username = www.downloadHandler.text;
            }
        }
    }
    public void opencoursepanel()
    {
        if (LS.Permissions == "1")
        {
            coursepanel_student.SetActive(true);
        }
        else if (LS.Permissions == "2")
        {
            coursepanel_teacher.SetActive(true);
        }
    }
    public void openresultpanel()
    {
        if (LS.Permissions == "1")
        {
            resultpanel_student.SetActive (true);
        }
        else if (LS.Permissions == "2")
        {
            resultpanel_teacher.SetActive (true);
        }
    }
}
