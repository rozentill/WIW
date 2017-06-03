using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;


public class Note : MonoBehaviour {

    public int type;
    public string soundName;

    float dropSpeed = 7f;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);

        if (GetInput())
            HitNote(true);
        else if (transform.localPosition.y < -550f)
            HitNote(false);         
    }

    bool GetInput()
    {
        if (transform.localPosition.y > -400f + GlobalData.noteTolerate || transform.localPosition.y < -400f - GlobalData.noteTolerate)
            return false;

        if (type == 0 && (Input.GetKeyDown(KeyCode.Alpha1)||BodyControl.isLeftStep))
            return true;

        if (type == 1 && (Input.GetKeyDown(KeyCode.Alpha2) || BodyControl.isClap))
            return true;

        if (type == 2 && (Input.GetKeyDown(KeyCode.Alpha3) || BodyControl.isRightStep))
            return true;

        return false;
    }

    void HitNote(bool success)
    {
        if (success)
        {
            GlobalData.successNoteCount++;
            GameObject.Find(soundName).GetComponent<AudioSource>().Play();
        }

        ShowRank(success);

        Destroy(gameObject);
    }

    void ShowRank(bool success)
    {
        string rankName;
        if (success)
            rankName = "RankP" + type;
        else
            rankName = "RankF" + type;

        Image rank = GameObject.Find(rankName).GetComponent<Image>();
        Sequence seq = DOTween.Sequence();
        seq.Append(rank.DOFade(1f, 0.1f));
        seq.AppendInterval(0.3f);
        seq.Append(rank.DOFade(0f, 0.1f));
    }
}
