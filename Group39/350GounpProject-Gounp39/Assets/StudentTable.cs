using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StudentTable : MonoBehaviour
{
    public Transform enterCon;
    public Transform enterTem;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    public TextAsset tad;

    [System.Serializable]
    public class studentdata
    {
        public string sid;
        public string sname;
    }

    [System.Serializable]
    public class studentdatalist
    {
        public studentdata[] SD;
    }

    public studentdatalist mystudentdatalist = new studentdatalist();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();

        enterTem.gameObject.SetActive(false);

        float templateheight = 35f;
        for (int i = 0; i < mystudentdatalist.SD.Length; i++)
        {
            text1.text = mystudentdatalist.SD[i].sid;
            text2.text = mystudentdatalist.SD[i].sname;
            Transform entryTransform = Instantiate(enterTem, enterCon);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateheight * i );
            
            entryTransform.gameObject.SetActive(true);

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ReadCSV()
    {
        string[] data = tad.text.Split(new string[] { ",", "\n"}, StringSplitOptions.None);

        int tableSize = data.Length / 2 - 1;
        mystudentdatalist.SD = new studentdata[tableSize];

        for(int i = 0; i < tableSize; i++)
        {
            mystudentdatalist.SD[i] = new studentdata();
            mystudentdatalist.SD[i].sid = data[2 * (i + 1) ];
            mystudentdatalist.SD[i].sname = data[2 * (i + 1) + 1];
        }
    }
}
