using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformLinear : Attacker
{
    public float spd, r0;   //spd: 속도상수, r0: 각도보정상수(0도를 보게 하는 초기 z각도)
    Rigidbody2D rb2d;
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        float angle = (transform.rotation.eulerAngles.z - r0) * Mathf.Deg2Rad;
        rb2d.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spd;
    }


    private new void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        if (col.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            rb2d.velocity = Vector2.zero;
        }
    }

}
