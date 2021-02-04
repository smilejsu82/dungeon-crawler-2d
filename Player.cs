using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Animator anim;
    public bool isMoving;

    public Transform gunFront;
    public Transform gunBack;
    public Transform gunLeft;
    public Transform gunRight;

    public GameObject bulletPrefab;

    private float elapsedTime;
    public float fireDelay = 0.5f;


    void Start()
    {
        this.anim = this.GetComponent<Animator>();
    }

    private float left;
    private float right;
    private float top;
    private float bottom;

    public void Init(float left, float right, float top, float bottom)
    {
        this.left = left;
        this.right = right;
        this.top = top;
        this.bottom = bottom;
    }

    void Update()
    {

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var dir = new Vector3(h, v, 0).normalized;

        var dis = dir * this.speed * Time.deltaTime;
        var bulletDir = Vector3.zero;

        Transform targetGunPos = gunFront;

        if (h > 0)
        {
            this.anim.Play("Right_Run");
            targetGunPos = this.gunRight;
            bulletDir = this.transform.right;
        }
        else if (h < 0)
        {
            this.anim.Play("Left_Run");
            targetGunPos = this.gunLeft;
            bulletDir = this.transform.right * -1;
        }

        if (h == 0 && v > 0)
        {
            this.anim.Play("Back_Run");
            targetGunPos = gunBack;
            bulletDir = this.transform.up;
        }
        else if (h == 0 && v < 0)
        {
            this.anim.Play("Front_Run");
            targetGunPos = gunFront;
            bulletDir = this.transform.up * -1;
        }

        if (h == 0 && v == 0)
        {
            var info = this.anim.GetCurrentAnimatorClipInfo(0);
            var name = info[0].clip.name;
            var strDir = name.Split('_')[0];
            var animName = string.Format("{0}_{1}", strDir, "Idle");
            this.anim.Play(animName);
            this.isMoving = false;

            if (strDir == "Front")
            {
                targetGunPos = gunFront;
                bulletDir = this.transform.up * -1;
            }
            else if (strDir == "Back")
            {
                targetGunPos = gunBack;
                bulletDir = this.transform.up;
            }
            else if (strDir == "Right")
            {
                targetGunPos = this.gunRight;
                bulletDir = this.transform.right;
            }
            else if (strDir == "Left")
            {
                targetGunPos = this.gunLeft;
                bulletDir = this.transform.right * -1;
            }
        }
        else
        {
            this.isMoving = true;
        }

        this.elapsedTime += Time.deltaTime;
        if (this.elapsedTime >= fireDelay)
        {
            this.elapsedTime = 0;
            Debug.DrawRay(targetGunPos.position, bulletDir * 3, Color.red, 1.0f);

            var bulletGo = Instantiate(this.bulletPrefab);
            bulletGo.transform.position = targetGunPos.position;
            var bullet = bulletGo.GetComponent<Bullet>();
            bullet.init(bulletDir);

        }

        this.transform.Translate(dis);

        if (this.isMoving)
        {
            float clampX = 0;
            float clampY = 0;

            if (this.transform.position.x < this.left || this.transform.position.x > this.right)
            {
                clampX = Mathf.Clamp(this.transform.position.x, this.left, this.right);
                var pos = this.transform.position;
                pos.x = clampX;
                this.transform.position = pos;
            }


            if (this.transform.position.y > this.top || this.transform.position.y < this.bottom)
            {
                clampY = Mathf.Clamp(this.transform.position.y, this.bottom, this.top);
                var pos = this.transform.position;
                pos.y = clampY;
                this.transform.position = pos;
            }

        }
    }
}
