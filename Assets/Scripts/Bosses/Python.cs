﻿using System.Collections;
using UnityEngine;

public class Python : Boss
{
    JointBody[] joints;
    public float hi, lo; // 공통 각도제한(max, min 순서)
    Transform tf;
    Vector2 head0;
    float relDeg;
    int rayMask;
    bool busy;
    public GameObject warnRay;

    public Cinemachine.CinemachineImpulseSource imsr;

    protected override void St()
    {
        busy = false;

        maxHp = 100000000;
        hp = maxHp;
        exp = 0;
        base.St();
        rayMask = 1 << LayerMask.NameToLayer("Map");
        joints = GetComponentsInChildren<JointBody>();
        dmgPos = joints[0].transform;
        for (int i = 0; i + 1 < joints.Length; i++)
        {
            joints[i].setTail(joints[i + 1]);
            joints[i].setAngleLim(lo, hi);
        }
        tf = FindObjectOfType<Player>().transform;
        head0 = joints[0].transform.position;
    }

    private void Update()
    {
        float dx = tf.position.x - joints[0].transform.position.x;
        float dy = tf.position.y - joints[0].transform.position.y;
        relDeg = Mathf.Atan(dy / dx) * Mathf.Rad2Deg;
        if (dx > 0) joints[0].rb2d.MoveRotation(0);
        else joints[0].rb2d.MoveRotation(Mathf.Clamp(relDeg, -90, 90));
        if (!busy)
        {
            joints[0].rb2d.MovePosition((head0 + joints[0].rb2d.position * 19) / 20);
            if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Snipe(dx, dy));
            if (Input.GetKeyDown(KeyCode.C)) { StartCoroutine(Shake()); }
        }
    }

    public IEnumerator Snipe(float dx, float dy)
    {
        dy += 0.5f;
        busy = true;
        var j0 = joints[0];
        j0.rb2d.constraints = RigidbodyConstraints2D.None;
        var rh = Physics2D.Raycast(joints[0].transform.position, new Vector2(dx, dy), float.PositiveInfinity, rayMask).point;
        j0.setSp(1);
        var ray = Instantiate(warnRay);
        ray.GetComponent<WarnRay>().t = 1.0f / SysManager.difficulty;
        ray.transform.localPosition = j0.transform.position;
        ray.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan(dy / dx) * Mathf.Rad2Deg);
        ray.transform.localScale = new Vector2(-5, 10);
        yield return new WaitForSeconds(1.0f / SysManager.difficulty);
        j0.rb2d.MovePosition(rh);
        j0.setSp(0);
        yield return new WaitForSeconds(1f);
        busy = false;
    }

    public IEnumerator Shake()
    {
        busy = true;
        var tail = joints[14].rb2d;
        tail.MoveRotation(80);
        yield return new WaitForSeconds(0.3f);
        tail.AddTorque(-10000);
        yield return new WaitForSeconds(0.2f);
        imsr.GenerateImpulse();
        if (po.onground) { 
            po.GetHit(0, 2);
            prb2d.AddForce(Vector2.up * 200);
        }
        busy = false;
    }

    public IEnumerator Spit()
    {
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator still(float time)
    {
        for (int i = 0; i + 1 < joints.Length; i++)
        {
            joints[i].rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        yield return new WaitForSeconds(time);
        for (int i = 0; i + 1 < joints.Length; i++)
        {
            joints[i].rb2d.constraints = RigidbodyConstraints2D.None;
        }
        joints[12].rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    protected override IEnumerator OnZero()
    {
        HPChange(0b100000000);
        yield return 0;
    }
}