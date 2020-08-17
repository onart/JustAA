using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooke : MonoBehaviour
{
    Animator anim;
    Vector2 dir;
    public float mag;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        float angle = (transform.rotation.eulerAngles.z - 90) * Mathf.Deg2Rad;
        dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        anim.SetTrigger("Str");
        var p = col.gameObject.GetComponentInParent<Player>();
        var rb = col.gameObject.GetComponentInParent<Rigidbody2D>();
        if (rb)
        {
            float vx = biggerMag(rb.velocity.x / 2, dir.x * mag);
            rb.velocity = new Vector2(vx, dir.y * mag);
            if (p) p.reserveVx(vx);
        }
    }

    float biggerMag(float a, float b)
    {
        return Mathf.Abs(a) > Mathf.Abs(b) ? a : b;
    }
}
