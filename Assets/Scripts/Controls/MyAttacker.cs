using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAttacker : MonoBehaviour
{
    public Vector2 force;       //미는 방향. 여기서 정하는 게 아니라 애니메이션에서 정해줄 것이다
    public int delta;           //주는 피해량. 위와 동일
    public Transform p;         //플레이어 localscale때문에.

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var en = col.gameObject.GetComponent<Enemy>();
            if (p.localScale.x < 0) force.x = -force.x;
            en.GetHit(delta, force);
        }
    }
}
