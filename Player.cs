using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Animator anim;

    void Start()
    {
        this.anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var dir = new Vector3(h, v, 0).normalized;

        var dis = dir * this.speed * Time.deltaTime;

        if (h > 0)
        {
            this.anim.Play("Right_Run");
        }
        else if (h < 0) {
            this.anim.Play("Left_Run");
        }

        if (h == 0 && v > 0)
        {
            this.anim.Play("Back_Run");
        }
        else if (h == 0 && v < 0) {
            this.anim.Play("Front_Run");
        }

        if (h == 0 && v == 0) {
            var info = this.anim.GetCurrentAnimatorClipInfo(0);
            var name = info[0].clip.name;
            var animName = string.Format("{0}_{1}", name.Split('_')[0], "Idle");
            this.anim.Play(animName);
        }

        this.transform.Translate(dis);
    }
}
