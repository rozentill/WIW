using UnityEngine;
using System.Collections;

public class BonusCreator : MonoBehaviour {

    public GameObject bonus;

    float dx = 11f, dy = 5f;

    int count = 78;

	// Use this for initialization
	void Start () {
	    
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(bonus);
            go.transform.SetParent(transform);

            int a = Random.Range(-1, 2);
            int b = Random.Range(-1, 2);

            go.transform.position = new Vector3(dx * a, dy * b, 110f + 20f * i);
            go.transform.localScale = Vector3.one;                
        }

        bonus.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
