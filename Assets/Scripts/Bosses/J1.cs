using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J1 : Boss
{
    const float speed = 4.0f;

    Collider2D body;
    int mapMask;
    bool busy;  //행동 중임을 나타냄. busy==true인 경우는 판단 과정을 거치지 않음

    protected override void St()
    {
        maxHp = 60 + 20 * SysManager.difficulty;
        hp = maxHp;
        body = GetComponent<Collider2D>();
        mapMask = 1 << LayerMask.NameToLayer("Map");
    }

    void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            anim.SetBool("GROUND", true);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            anim.SetBool("GROUND", false);
        }
    }

}
