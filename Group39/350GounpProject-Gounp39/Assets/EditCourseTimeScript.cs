using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class EditCourseTimeScript : MonoBehaviour
{
    // Start is called before the first frame update
    int dayint;
    int timeint;
    public string courseday;
    public string coursetime;
    public TextMeshProUGUI courseidtext;
    public string courseid;
    public string NCT = "";
    public TextMeshProUGUI NCTtext;
    public GameObject DayErrortext;
    public GameObject TimeErrortext;
    public GameObject Loading;
    public GameObject Finish;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (dayint)
        {
            case 0: courseday = ""; break;
                case 1: courseday = "Mon"; break;
                case 2: courseday = "Tue"; break;
                case 3: courseday = "Wen"; break;
                case 4: courseday = "Thu"; break;
                case 5: courseday = "Fri"; break;
        }

        switch (timeint)
        {
            case 0: coursetime = ""; break;
                case 1: coursetime = "09:00-10:50"; break;
                case 2: coursetime = "10:00-11:50"; break;
                case 3: coursetime = "11:00-12:50"; break;
                case 4: coursetime = "12:00-13:50"; break;
                case 5: coursetime = "13:00-14:50"; break;
                case 6: coursetime = "14:00-15:50"; break;
                case 7: coursetime = "15:00-16:50"; break;
                case 8: coursetime = "16:00-17:50"; break;
                case 9: coursetime = "17:00-18:50"; break;
        }

        NCT = courseday + " " + coursetime;
        NCTtext.text = NCT;
    }
    public void inputday(int gd)
    {
        dayint = gd;
    }
    public void inputtime(int gt)
    {
        timeint = gt;
    }
    public void readcourseid(TextMeshProUGUI s)
    {
        courseid = s.text;
        courseidtext.text = courseid;
    }
    public void CourseTimeEdit()
    {
        if(dayint!=0 && timeint != 0)
        {
            DayErrortext.SetActive(false);
            TimeErrortext.SetActive(false);
            Loading.SetActive(true);
            StartCoroutine(CTE());
            StartCoroutine(FinishS());
        }
        if(dayint == 0)
        {
            DayErrortext.SetActive(true);
        }
        else
        {
            DayErrortext.SetActive(false);
        }
        if(timeint == 0)
        {
            TimeErrortext.SetActive(true);
        }
        else
        {
            TimeErrortext.SetActive(false);
        }  
    }
    IEnumerator CTE()
    {
        WWWForm form = new WWWForm();
        //int Row = 2;
        form.AddField("method", "EditCourseTime");
        form.AddField("cid", courseid);
        form.AddField("EditData", NCT);
        //form.AddField("Row", Row);

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
    IEnumerator FinishS()
    {
        yield return new WaitForSeconds(1.5f);
        Finish.SetActive(true);
    }
}
