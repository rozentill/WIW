using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {

    public Image fillImage;

    private void Update()
    {
        if (fillImage.fillAmount == 1f)
            SceneManager.LoadScene(1);
    }


}
