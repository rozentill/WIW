using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class UIState : MonoBehaviour {

    PlayerManager manager;

    public Transform healthObject, powerObject;

    GameObject[] health = new GameObject[5];
    GameObject[] power = new GameObject[5];

    public Text score;
    

	// Use this for initialization
	void Start ()
    {
        manager = FindObjectOfType<PlayerManager>();

        for (int i = 0; i < healthObject.childCount; i++)
            health[i] = healthObject.GetChild(i).gameObject;

        for (int i = 0; i < powerObject.childCount; i++)
            power[i] = powerObject.GetChild(i).gameObject;

    }
	
	// Update is called once per frame
	void Update ()
    {

        score.text = GlobalData.score.ToString();

        for (int i = 0; i < 5 - manager.hp; i++)
        {
            health[i].SetActive(false);
        }



        for (int i = 0; i < manager.power; i++)
        {
            power[i].SetActive(true);
        }
        for (int i = manager.power; i < 5; i++)
        {
            power[i].SetActive(false);
        }

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
