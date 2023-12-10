using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static CourseInfoScript;

public class ResultScript : MonoBehaviour
{
    public List<string> student;
    int col = 2;
    public string userid;
    public string finduser;
    public string findusertot;
    public LoginScript LS;
    public TextMeshProUGUI resulttext;
    public TextMeshProUGUI resulttextteacher;

    public Transform enterCon;
    public Transform enterTem;

    public TextMeshProUGUI buttontext;
    public TextMeshProUGUI studenttext;

    public TextMeshProUGUI totalresulttext_student;
    public TextMeshProUGUI totalresulttext_teacher;

    public int count;
    public int countc;
    public int counthc;

    public string result;
    public float totalresult;


    // Start is called before the first frame update
    void Start()
    {
        userid = LS.useridinput;
        //findusercourse();
        findstudent();
        //printbutton();
    }

    // Update is called once per frame
    void Update()
    {
        userid = LS.useridinput;
    }
    public void finduserresult()
    {
        resulttext.text = "Loading Data";
        StartCoroutine(uc());
    }
    IEnumerator uc()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "Result");
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
                resulttext.text = www.downloadHandler.text;
            }
        }
    }
    public void findstudent()
    {
        StartCoroutine(fs());
    }
    IEnumerator fs()
    {
        student = new List<string>();
        while (true)
        {
            WWWForm form = new WWWForm();
            form.AddField("method", "FindStudent");
            form.AddField("col", col);
            using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    if (www.downloadHandler.text != "")
                    {
                        student.Add(www.downloadHandler.text);
                        col++;
                    }
                    else
                    {
                        //Debug.Log("Form upload complete!");
                        count++;
                        break;
                    }
                }
            }
        }
    }
    public void printbutton()
    {
        if (count == 1)
        {
            enterTem.gameObject.SetActive(false);
            float templateheight = 100f;
            for (int i = 0; i < student.Count; i++)
            {
                buttontext.text = student[i];

                Transform entryTransform = Instantiate(enterTem, enterCon);

                RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

                entryRectTransform.anchoredPosition = new Vector2(0, -templateheight * i);

                entryTransform.gameObject.SetActive(true);
            }
            count++;
        }
    }
    public void result_teacher(TextMeshProUGUI s)
    {
         finduser = s.text;
        studenttext.text = s.text + " result:";
        resulttextteacher.text = "Loading Data";
        StartCoroutine(rt());
    }
    IEnumerator rt()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "Result");
        form.AddField("userid", finduser);
        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                resulttextteacher.text = www.downloadHandler.text;
            }
        }
    }
    public void findusertotalresult()
    {
        StartCoroutine(utr());
    }
    IEnumerator utr()
    {
        totalresult = 0f;
        countc = 0;
        counthc = 0;
        totalresulttext_student.text = "Loading total result";
        while (true)
        {
            WWWForm form = new WWWForm();
            form.AddField("method", "totalresult");
            form.AddField("userid", userid);
            if (countc == 0)
            {
                form.AddField("course", "C101");
            }
            else if (countc == 1)
            {
                form.AddField("course", "C102");
            }
            else if (countc == 2)
            {
                form.AddField("course", "E103");
            }
            else if (countc == 3)
            {
                form.AddField("course", "C104");
            }
            else if (countc == 4)
            {
                form.AddField("course", "C105");
            }
            else if (countc == 5)
            {
                form.AddField("course", "C106");
            }

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
                    if (www.downloadHandler.text != "0")
                    {
                        counthc++;
                    }
                    result = www.downloadHandler.text;
                    totalresult += float.Parse(result);
                    countc++;
                    if (countc == 6)
                    {
                        totalresult = totalresult / counthc;
                        if (totalresult>85)
                        {
                            totalresulttext_student.text = "Total Result: A";
                        }
                        else if (totalresult > 70)
                        {
                            totalresulttext_student.text = "Total Result: B";
                        }
                        else if (totalresult > 55)
                        {
                            totalresulttext_student.text = "Total Result: C";
                        }
                        else if (totalresult > 40)
                        {
                            totalresulttext_student.text = "Total Result: D";
                        }
                        else
                        {
                            totalresulttext_student.text = "Total Result: F";
                        }
                        break;
                    }
                }
            }
        }
    }
    public void findusertotalresult_teacher(TextMeshProUGUI t)
    {
        findusertot = t.text;
        StartCoroutine(utrt());
    }
    IEnumerator utrt()
    {
        totalresult = 0f;
        countc = 0;
        counthc = 0;
        totalresulttext_teacher.text = "Loading total result";
        while (true)
        {
            WWWForm form = new WWWForm();
            form.AddField("method", "totalresult");
            form.AddField("userid", findusertot);
            if (countc == 0)
            {
                form.AddField("course", "C101");
            }
            else if (countc == 1)
            {
                form.AddField("course", "C102");
            }
            else if (countc == 2)
            {
                form.AddField("course", "E103");
            }
            else if (countc == 3)
            {
                form.AddField("course", "C104");
            }
            else if (countc == 4)
            {
                form.AddField("course", "C105");
            }
            else if (countc == 5)
            {
                form.AddField("course", "C106");
            }

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
                    if (www.downloadHandler.text != "0")
                    {
                        counthc++;
                    }
                    result = www.downloadHandler.text;
                    totalresult += float.Parse(result);
                    countc++;
                    if (countc == 6)
                    {
                        totalresult = totalresult / counthc;
                        if (totalresult > 85)
                        {
                            totalresulttext_teacher.text = "Total Result: A";
                        }
                        else if (totalresult > 70)
                        {
                            totalresulttext_teacher.text = "Total Result: B";
                        }
                        else if (totalresult > 55)
                        {
                            totalresulttext_teacher.text = "Total Result: C";
                        }
                        else if (totalresult > 40)
                        {
                            totalresulttext_teacher.text = "Total Result: D";
                        }
                        else
                        {
                            totalresulttext_teacher.text = "Total Result: F";
                        }
                        break;
                    }
                }
            }
        }
    }
}
