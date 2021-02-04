using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 4;


    public Vector3 dir;
    public void init(Vector3 dir)
    {
        this.dir = dir;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.dir != Vector3.zero)
            this.transform.Translate(this.dir * this.speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
