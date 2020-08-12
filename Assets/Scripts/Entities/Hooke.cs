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
        var rb = col.gameObject.GetComponentInParent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity = new Vector2(biggerMag(rb.velocity.x, dir.x * mag), dir.y * mag);
        }
    }

    float biggerMag(float a, float b)
    {
        return Mathf.Abs(a) > Mathf.Abs(b) ? a : b;
    }
}
