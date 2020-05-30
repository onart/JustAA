using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAttacker : MonoBehaviour
{
    public Vector2 force;       //미는 방향. 여기서 정하는 게 아니라 애니메이션에서 정해줄 것이다
    public int delta;           //주는 피해량. 위와 동일
    public Transform p;         //플레이어 localscale때문에.

    //방침 추가 : 플레이어는 가볍게, 적 및 오브젝트는 무겁게 하여 플레이어의 움직임에 적이 영향을 받지 않도록 함

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var en = col.gameObject.GetComponent<Enemy>();
            if (p.localScale.x < 0) force.x = -force.x;
            en.GetHit(delta, force*100);
        }
        else if (col.gameObject.tag == "plat")
        {
            var plat = col.gameObject.GetComponent<Rigidbody2D>();
            if (p.localScale.x < 0) force.x = -force.x;
            plat.AddForce(force*250);
        }
    }
}
