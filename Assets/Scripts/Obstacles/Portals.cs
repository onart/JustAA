using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : HPChanger
{
    public Transform targetPos;

    protected override void Act()
    {
        p.transform.position = targetPos.position;
        var g = p.gameObject.GetComponent<Rigidbody2D>();
        g.velocity = Vector2.zero;
        var dam = delta << SysManager.difficulty;
        p.HpChange(-dam);
        p.reserveVx(0);
        p.reserveVy(0);
    }
}
