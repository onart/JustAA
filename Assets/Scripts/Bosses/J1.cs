using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class J1 : Boss
{
    const float speed = 4.0f;

    Collider2D body;
    int mapMask;
    public bool busy;  //행동 중임을 나타냄. busy==true인 경우는 판단 과정을 거치지 않음
    public GameObject cutFX, imFX;  // 베기, 찌르기 프리팹   
    Vector2 dxy;       //플레이어와의 위치 차이
    Vector2 basic, basic_x;      //기본 스케일, x반전

    protected override void St()
    {
        basic = new Vector2(0.3f, 0.3f);
        basic_x = new Vector2(-0.3f, 0.3f);
        maxHp = 60 + 20 * SysManager.difficulty;
        hp = maxHp;
        body = GetComponent<Collider2D>();
        mapMask = 1 << LayerMask.NameToLayer("Map");
    }

    void Update()
    {
        if (!busy)
        {
            
        }
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

    void getDist()
    {
        dxy = p.position - transform.position;
    }

    void combo() //한 번 휘두를 때마다 호출(즉 애니메이션 1루프에서 2회 호출됨)
    {
        int d = SysManager.difficulty;
        if (d > 1)
        {
            getDist();
            if (dxy.x > 0) transform.localScale = basic;
            else transform.localScale = basic_x;
            if (d > 2)
            {
                rb2d.velocity += new Vector2(transform.localScale.x * 15, 0);
            }
        }
    }

    void slash(int delta)
    {         
        var fx = Instantiate(cutFX);
        fx.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        fx.transform.position = transform.position + new Vector3(transform.localScale.x * 2, transform.localScale.y);
        var at = fx.GetComponent<Attacker>();
        at.delta = delta;
        at.force *= new Vector2(200, 70);
        if (transform.localScale.x > 0) at.face = 1;
        else at.face = -1;
        fx.GetComponent<CircleCollider2D>().enabled = true;
    }
    
}
