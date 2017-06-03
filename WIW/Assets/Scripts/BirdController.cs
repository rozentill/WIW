using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour {

    PlayerManager manager;
    
    Animator animator;

    Vector3 target;

    float verticalMovement = 6.0f, horizonalMovement = 12.0f;

    float speed = 150f;

    Vector3 currentVelocity1;
    Vector3 currentVelocity2;

    float smoothTime = 0.2f;

    public bool isUp;
    public bool isDown;
    public bool isLeft;
    public bool isRight;

    void Awake()
    {
        manager = GetComponentInParent<PlayerManager>();
        animator = GetComponent<Animator>();
    }
    
	// Update is called once per frame
	void Update () {
        
        isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || BodyControl.isUp;
        isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || BodyControl.isDown;
        isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || BodyControl.isLeft;
        isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || BodyControl.isRight;

        if (!GlobalData.detectInput)
        {
            isUp = false;
            isDown = false;
            isLeft = false;
            isRight = false;
        }

        if (GlobalData.getHit || GlobalData.stopFly || Time.timeScale == 0f)
            return;

        target = Vector3.zero;

        if (isUp)
            target += Vector3.up * verticalMovement;

        if (isDown)
            target += Vector3.down * verticalMovement;

        if (isLeft)
            target += Vector3.left * horizonalMovement;

        if (isRight)
            target += Vector3.right * horizonalMovement;

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref currentVelocity1, smoothTime);        



        Vector3 cameraAngle = Camera.main.transform.eulerAngles;

        if (target != Vector3.zero)
            cameraAngle.z = Mathf.LerpAngle(cameraAngle.z, -target.x, 0.07f);
        else
            cameraAngle.z = Mathf.LerpAngle(0f, cameraAngle.z, 1f / 1.07f);

        Camera.main.transform.localRotation = Quaternion.Euler(cameraAngle);        
    }

    void OnTriggerEnter(Collider collider)
    {
        //print(collider.name);



        if (collider.gameObject.layer == LayerMask.NameToLayer("Bonus"))
        {
            if (manager.RaisePower())
                animator.speed += 1f;            

            Destroy(collider.gameObject);                        
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
             if (manager.GetHit())
            {
                animator.speed = 1f;
                animator.Play("Hit");
            }
        }

        if (collider.gameObject.layer == LayerMask.NameToLayer("StopPlane"))
        {
            animator.Stop();
            manager.StopFly();
            FindObjectOfType<UIState>().Close();
        }
    }
}
