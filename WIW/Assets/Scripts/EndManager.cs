using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    
    public Text score;
    public Image fillImage;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
        score.text = GlobalData.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (fillImage.fillAmount == 1f)
            SceneManager.LoadScene(0);
    }
}
