using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class UITutorial : MonoBehaviour {


    public Image fillImage, tutorialImage;
    public CanvasGroup story, tutorial;
    public GameObject tutorialPanel, statePanel;

    public Sprite[] t;
    private int count = 0;

    private bool startTutorial = false;
    private float passTime = 0f;
    private float inputWaitTime = 1f;

    // Update is called once per frame
    void Update ()
    {
	    if (fillImage.fillAmount == 1f && !startTutorial)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(story.DOFade(0f, 0.3f));
            seq.Append(tutorial.DOFade(1f, 0.3f));
            startTutorial = true;

            Cursor.visible = false;
        }

        if (startTutorial && count <= 2)
        {
            passTime += Time.unscaledDeltaTime;
            if (passTime - 0.6f > GlobalData.tutorialWaitTime)
            {
                Sequence seq = DOTween.Sequence();
                seq.Append(tutorial.DOFade(0f, 0.3f));
                seq.AppendCallback(ChangeTutorialImage);

                passTime = 0f;
            }
        }

        if (startTutorial && count >= 3 && count <= 6)
        {
            passTime += Time.unscaledDeltaTime;
            if (passTime - 0.2f > inputWaitTime && Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
                tutorialImage.sprite = t[count];
                GlobalData.detectInput = false;
                Sequence seq = DOTween.Sequence();
                seq.Append(tutorial.DOFade(1f, 0.3f));
                seq.AppendCallback(() => { GlobalData.detectInput = true; });
            }
            else if (GetCorrectInput() && Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
                tutorial.DOFade(0f, 0.2f);
                count++;
                passTime = 0f;
            }
        }

        if (startTutorial && count == 7)
        {
            startTutorial = false;
            statePanel.SetActive(true);
            tutorialPanel.SetActive(false);
        }
    }

    void ChangeTutorialImage()
    {
        count++;
        if (count < 3)
        {
            tutorialImage.sprite = t[count];
            tutorial.DOFade(1f, 0.3f);
        }
        else
        {
            FindObjectOfType<PlayerManager>().StartGame();
        }
    }

    bool GetCorrectInput()
    {
        if (count == 3 && FindObjectOfType<BirdController>().isUp)
            return true;

        if (count == 4 && FindObjectOfType<BirdController>().isDown)
            return true;

        if (count == 5 && FindObjectOfType<BirdController>().isLeft)
            return true;

        if (count == 6 && FindObjectOfType<BirdController>().isRight)
            return true;

        return false;
    }
}
