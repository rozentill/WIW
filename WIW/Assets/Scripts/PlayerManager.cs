using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerManager : MonoBehaviour {

    int speed = 15;

    public int hp = 1;
    public int power = 0;

    int score = 0;
    
    public AudioSource bgm, bonusSound, hitSound;

    float invincibleTime = 2f;
    float passTime = 0f;

    float slowDownDistance = 83f;
    float slowDownTime = 3f;
    float slowDownVelocity = 0f;

    public GameObject tutorialTwoPanel;

    void Start()
    {
        score = 0;
        GlobalData.Init();
        Time.timeScale = 0f;
    }
    
	void Update ()
    {
        if (GlobalData.slowDown || GlobalData.stopFly)
            return;

        if (passTime < 1f)
            GlobalData.getHit = true;
        else
            GlobalData.getHit = false;

        if (!GlobalData.getHit)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * (speed + power * 4f));
            IncreaseScore(speed + power * 9);
        }
        
        passTime += Time.deltaTime;

    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        bgm.Play();
    }

    public bool RaisePower()
    {
        bonusSound.Play();

        if (power < 5)
        {
            power++;

            return true;
        }
        else
        {
            IncreaseScore(21000);
            return false;
        }
    }

    public bool GetHit()
    {
        if (passTime < invincibleTime)
        {
            return false;
        }
        else
        {
            hitSound.Play();

            passTime = 0f;
            
            power = 0;
            hp--;
            if (hp <= 0)
            {
                GlobalData.stopFly = true;
                StartCoroutine(GameOver());
            }

            return true;
        }

    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f); 
        SceneManager.LoadScene(2);
    }
    
    public void StopFly()
    {
        GlobalData.detectInput = false;
        GlobalData.slowDown = true;

        float trueSpeed = speed + power * 5f;
        slowDownVelocity = trueSpeed * trueSpeed / 2f / slowDownDistance;

        StartCoroutine(SlowDown(trueSpeed));
    }

    IEnumerator SlowDown(float speed)
    {
        speed -= Time.deltaTime * slowDownVelocity;
        yield return -1;
        if (speed <= 0f)
        {
            GlobalData.stopFly = true;
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DORotate(new Vector3(0f, 90f, 0f), 1f, RotateMode.WorldAxisAdd));
            seq.Append(transform.DOMove(new Vector3(12f, -10f, 1744f), 2f));
            seq.AppendCallback(()=> { bgm.DOFade(0.1f, 1f); tutorialTwoPanel.SetActive(true); });

            IncreaseScore(hp * 100000);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            StartCoroutine(SlowDown(speed));
        }
    }

    void IncreaseScore(int s)
    {
        score += s;
        GlobalData.score = score;
    }

}
