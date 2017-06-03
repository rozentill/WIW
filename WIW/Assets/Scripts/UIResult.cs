using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIResult : MonoBehaviour {

    public Image fillImage;

    public GameObject[] resultText;

    void Start()
    {
        Cursor.visible = true;
        resultText[GlobalData.result].SetActive(true);
    }

    void Update()
    {
        if (fillImage.fillAmount == 1f)
        {
            if (GlobalData.result == 0)
                SceneManager.LoadScene(0);
            else if (GlobalData.result == 1)
                SceneManager.LoadScene(2);
        }
    }


}
