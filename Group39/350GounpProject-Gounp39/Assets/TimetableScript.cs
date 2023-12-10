using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TimetableScript : MonoBehaviour
{
    public string userid;
    public LoginScript LS;
    public TextMeshProUGUI timetabletext;
    // Start is called before the first frame update
    void Start()
    {
        userid = LS.useridinput;
        //findusertimetable();
    }

    // Update is called once per frame
    void Update()
    {
        userid = LS.useridinput;
    }
    public void findusertimetable()
    {
        timetabletext.text = "Loading Data";
        StartCoroutine(ut());
    }
    IEnumerator ut()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "Timetable");
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
                timetabletext.text = www.downloadHandler.text;
            }
        }
    }
}
