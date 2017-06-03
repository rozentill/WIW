using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class UIMusic : MonoBehaviour {

    public Text timeText;


    public Transform noteRoot;
    public Transform[] note;
    int[] noteType = { 0, 2, 0, 2, 1, 0, 2, 1, 1, 0, 2, 1, -1};
    float[] noteInterval = { 0f, 1f, 1f, 0.5f, 0.5f, 1f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 3f};
    float passTime = 0f;

    int dropCount = 0;

    public GameObject musicPanel, resultPanel;

    bool isSuccess = false;
    float remainTime = 45f;

    

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        remainTime -= Time.deltaTime;

        timeText.text = ((int)remainTime).ToString();

        if (isSuccess || remainTime < 0f)
        {
            if (isSuccess)
                GlobalData.result = 1;
            else if (remainTime < 0f)
                GlobalData.result = 0;

            resultPanel.SetActive(true);
            musicPanel.SetActive(false);      
        }

        passTime += Time.deltaTime;
        if (passTime > noteInterval[dropCount])
        {
            if (noteType[dropCount] >= 0)
            {
                Transform n = (Transform)Instantiate(note[noteType[dropCount]], noteRoot);
                n.localPosition = new Vector3(n.position.x, 600f, 0f);
                n.localEulerAngles = Vector3.zero;
                n.localScale = Vector3.one;
            }
            

            dropCount++;   
            passTime = 0f;
        }

        if (dropCount >= noteType.Length)
        {
            if (GlobalData.successNoteCount >= GlobalData.requireNoteCount)
                isSuccess = true;
            else
                GlobalData.successNoteCount = 0;

            dropCount = 0;
        }
    }
}
