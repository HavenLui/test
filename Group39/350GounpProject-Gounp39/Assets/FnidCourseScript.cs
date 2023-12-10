using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FnidCourseScript : MonoBehaviour
{
    public LoginScript loginUser;
    public TextMeshProUGUI text1;
    // Start is called before the first frame update
    void Start()
    {
        //FindingCouurse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FindingCouurse()
    {
        StartCoroutine(Finding());
    }
    IEnumerator Finding()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "FC");
        form.AddField("UserID", loginUser.useridinput);

        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbyIN0bmLmQoSI7ugG8_KNe4nTC4r8CocVSPyQPDRufHuRhRcuO-UOjDdhi3sz-mTjjr/exec", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                text1.text = www.downloadHandler.text;
            }
        }
    }
}
