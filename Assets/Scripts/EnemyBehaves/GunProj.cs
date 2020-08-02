﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProj : Attacker // 적측 발사체
{
    public float span;  //총알의 수명

    private void Start()
    {
        Invoke("ProjHit", span);
        float angle = transform.eulerAngles.z;
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 4;
        Debug.Log(transform.eulerAngles.z);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Invoke("ProjHit", 0.05f);
    }

    void ProjHit()
    {
        Destroy(gameObject);
    }
}
