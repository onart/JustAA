using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynaPlat : MonoBehaviour
{
    Rigidbody2D rb2d;
    BoxCollider2D c2;
    int min;
    public Vector2 v0;
    Player p;
    CapsuleCollider2D cc2d;
    int mapMask;
    bool cools;

    // Start is called before the first frame update
    void Start()
    {
        cools = true;
        rb2d = GetComponent<Rigidbody2D>();
        c2 = GetComponent<BoxCollider2D>();
        rb2d.velocity = v0;
        min = 1;
        mapMask = 1 << LayerMask.NameToLayer("Map");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Map") || col.gameObject.layer == LayerMask.NameToLayer("Foreground")) 
        {
            min = -min;
            rb2d.velocity = min * v0;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!p) p = col.gameObject.GetComponent<Player>();
            if (!cc2d) cc2d = col.gameObject.GetComponent<CapsuleCollider2D>();
            p.reserveVx(min * v0.x);
            if (cools && Physics2D.IsTouchingLayers(cc2d, mapMask))    //이쪽 코드 : 끼임 및 벽뚫기 방지
            {
                if (v0.y != 0)
                {
                    min = -min;
                    rb2d.velocity = min * v0;
                    cools = false;
                    CancelInvoke();
                    Invoke("regen", 0.3f);
                }
            }
        }
    }

    void regen()
    {
        cools = true;
    }
}
