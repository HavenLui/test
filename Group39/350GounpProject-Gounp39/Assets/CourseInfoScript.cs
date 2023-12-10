using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static StudentTable;

public class CourseInfoScript : MonoBehaviour
{
    public Transform enterCon;
    public Transform enterTem;
    public TextMeshProUGUI buttontext;
    public TextMeshProUGUI nametext;

    public Transform enterCon_teacher;
    public Transform enterTem_teacher;
    public TextMeshProUGUI buttontext_teacher;
    public TextMeshProUGUI nametext_teacher;

    public TextMeshProUGUI courseidtext;
    public TextMeshProUGUI coursenametext;
    public TextMeshProUGUI coursetimetext;
    public TextMeshProUGUI courseinfotext;

    public TextMeshProUGUI courseidtext_teacher;
    public TextMeshProUGUI coursenametext_teacher;
    public TextMeshProUGUI coursetimetext_teacher;
    public TextMeshProUGUI courseinfotext_teacher;

    public string courseid;


    public TextAsset tad;

    [System.Serializable]
    public class coursedata
    {
        public string cid;
        public string cname;
        public string ctime;
    }

    [System.Serializable]
    public class coursedatalist
    {
        public coursedata[] CD;
    }

    public coursedatalist mycoursedatalist = new coursedatalist();
    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();

        enterTem.gameObject.SetActive(false);
        enterTem_teacher.gameObject.SetActive(false);

        float templateheight = 130f;
        for (int i = 0; i < mycoursedatalist.CD.Length; i++)
        {
            buttontext.text = mycoursedatalist.CD[i].cid;
            nametext.text = mycoursedatalist.CD[i].cname;

            buttontext_teacher.text = mycoursedatalist.CD[i].cid;
            nametext_teacher.text = mycoursedatalist.CD[i].cname;

            Transform entryTransform = Instantiate(enterTem, enterCon);
            Transform entryTransform_teacher = Instantiate(enterTem_teacher, enterCon_teacher);

            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            RectTransform entryRectTransform_teacher = entryTransform_teacher.GetComponent<RectTransform>();

            entryRectTransform.anchoredPosition = new Vector2(0, -templateheight * i);
            entryRectTransform_teacher.anchoredPosition = new Vector2(0, -templateheight * i);

            entryTransform.gameObject.SetActive(true);
            entryTransform_teacher.gameObject.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ReadCSV()
    {
        string[] data = tad.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 4 - 1;
        mycoursedatalist.CD = new coursedata[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            mycoursedatalist.CD[i] = new coursedata();
            mycoursedatalist.CD[i].cid = data[4 * (i + 1)];
            mycoursedatalist.CD[i].cname = data[4 * (i + 1) + 1];
            //mycoursedatalist.CD[i].ctime = data[4 * (i + 1) + 2];
        }
    }
    public void readstring(TextMeshProUGUI s)
    {
        courseid = s.text;
        courseidtext.text = courseid;
        courseidtext_teacher.text = courseid;
        StartCoroutine(readcoursename());
        StartCoroutine(readcoursetime());
        StartCoroutine(readcourseinfo());
    }
    IEnumerator readcoursename()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "courseinfo");
        form.AddField("CID", courseid);
        form.AddField("Row", 2);

        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                coursenametext.text = www.downloadHandler.text;
                coursenametext_teacher.text = www.downloadHandler.text;
            }
        }
    }
    IEnumerator readcoursetime()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "courseinfo");
        form.AddField("CID", courseid);
        form.AddField("Row", 3);

        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                coursetimetext.text = www.downloadHandler.text;
                coursetimetext_teacher.text = www.downloadHandler.text;
            }
        }
    }
    IEnumerator readcourseinfo()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "courseinfo");
        form.AddField("CID", courseid);
        form.AddField("Row", 4);

        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                courseinfotext.text = www.downloadHandler.text;
                courseinfotext_teacher.text = www.downloadHandler.text;
            }
        }
    }
}
