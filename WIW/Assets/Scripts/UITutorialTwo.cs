using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class UITutorialTwo : MonoBehaviour {

    public Image fillImage, tutorialImage;
    public CanvasGroup story, tutorial;
    public GameObject tutorialPanel, musicPanel;

    public Sprite[] t;
    private int count = 0;

    private bool startTutorial = false;
    private float passTime = 0f;

    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
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

        if (startTutorial && count == 3)
        {
            startTutorial = false;
            musicPanel.SetActive(true);
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
    }
}
