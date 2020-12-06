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
    public GameObject thr;          // 나이프 프리팹
    Vector2 dxy;       //플레이어와의 위치 차이
    Vector2 basic, basic_x;      //기본 스케일, x반전
    int hand;           //손에 든 무기의 수
    public MapEv ev;

    public Transform knv1, knv2;

    protected override void St()
    {
        basic = new Vector2(0.3f, 0.3f);
        basic_x = new Vector2(-0.3f, 0.3f);
        maxHp = 60 + 20 * SysManager.difficulty;
        hp = maxHp;
        body = GetComponent<Collider2D>();
        mapMask = 1 << LayerMask.NameToLayer("Map");
        hand = 2;
    }

    void Update()
    {
        anim.SetFloat("SPD", Mathf.Abs(rb2d.velocity.x));
        if (!busy && anim.GetBool("GROUND"))
        {
            getDist();
            switch (Mathf.Floor(Mathf.Abs(dxy.x)))
            {
                case 0:
                    if (Mathf.Abs(dxy.x) <= 0.7f) anim.SetTrigger("ATK");
                    else goto case 1;
                    break;
                case 1:
                case 2:
                    if (Random.Range(0, 4) != 0)
                    {
                        setVX(dxy.x * 2);
                        anim.SetFloat("SPD", Mathf.Abs(dxy.x));
                        busy = true;
                        CancelInvoke(nameof(unlock));
                        Invoke(nameof(unlock), 1);
                        //걸어서 접근
                    }
                    else
                    {
                        backJump();
                        //백점프
                    }
                    break;
                case 3:
                case 4:
                    if (Random.Range(0, 3) != 0)
                    {
                        anim.SetTrigger("CHARGE");
                        //돌진
                    }
                    else
                    {
                        backJump();
                        //백점프
                    }
                    break;
                default:
                    if (SysManager.difficulty == 3 && knv1 == null)
                    {
                        anim.SetTrigger("THROW");
                    }
                    else {
                        setVX(dxy.x);
                        busy = true;
                        anim.SetFloat("SPD", Mathf.Abs(dxy.x));
                        CancelInvoke(nameof(unlock));
                        Invoke(nameof(unlock), 1);
                        //걸어서 접근
                    }
                    break;
            }
            busy = true;
            //캐릭터와의 x,y좌표 차이에 따라 판단이 진행. 기준: 0~1: 근접공격, 1~3: 걸어서 접근하거나 백점프로 떨어짐, 3~5: 백점프로 떨어지거나 돌진으로 접근, 5~: 무기를 던지기만 함(최대 4개??)
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            anim.SetBool("GROUND", true);
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (hand == 0) anim.SetTrigger("DOWN");
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
        if (dxy.x * transform.localScale.x < 0) FaceBack();
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
                rb2d.velocity += new Vector2(dxy.x * 15, 0);    // 하다 밋밋하면 y좌표까지 움직일지도 모름
            }
        }
    }

    void slash(int delta)
    {         
        var fx = Instantiate(cutFX);
        fx.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        fx.transform.position = transform.position + new Vector3(transform.localScale.x * 2, transform.localScale.y);
        var at = fx.GetComponent<Attacker>();
        at.delta = delta * SysManager.difficulty / 3 + 1;
        at.force *= new Vector2(80, 100);
        if (transform.localScale.x > 0) at.face = 1;
        else at.face = -1;
        fx.GetComponent<CircleCollider2D>().enabled = true;
    }

    Quaternion float2Q(float ang)
    {
        if (transform.localScale.x > 0)
        {
            return Quaternion.AngleAxis(ang - 25, Vector3.forward);
        }
        else
        {
            return Quaternion.AngleAxis(180 + ang - 25, Vector3.forward);
        }
    }

    void thrower()
    {
        var angle1 = float2Q(Mathf.Atan(dxy.y / dxy.x) * Mathf.Rad2Deg + 20);
        var angle2 = float2Q(Mathf.Atan(dxy.y / dxy.x) * Mathf.Rad2Deg);
        knv1 = Instantiate(thr, transform.position + new Vector3(0, 0.4f), angle1).transform;
        knv2 = Instantiate(thr, transform.position + new Vector3(0, 0.4f), angle2).transform;
    }


    void charge1()
    {
        var fx = Instantiate(imFX, transform);
        fx.transform.position = transform.position + new Vector3(transform.localScale.x * 2, transform.localScale.y);
        var at = fx.GetComponent<Attacker>();
        at.delta = 10 * SysManager.difficulty;
        at.force *= new Vector2(200, 100);
        fx.transform.localScale = new Vector2(-1, 1);
        if (transform.localScale.x > 0) { 
            at.face = 1;
        }
        else
        {
            at.face = -1;
        }
        busy = true;
        if (transform.localScale.x < 0)
        {
            setV(-10, 1.0f);
        }
        else
        {
            setV(10, 1.0f);
        }
    }

    void charge2()
    {
        if (transform.localScale.x < 0)
        {
            setVX(-2);
        }
        else
        {
            setVX(2);
        }
    }

    public override void GetHit(int delta, Vector2 force)
    {
        base.GetHit(delta, force);
        if (!busy) anim.SetTrigger("HIT");
    }

    void backJump()
    {
        if (transform.localScale.x < 0)
        {
            if (Physics2D.Raycast((Vector2)transform.position, Vector2.right, 1.5f, LayerMask.GetMask("Map")).collider == null)
            {
                setV(3, 1.5f);
            }
        }
        else
        {
            if (Physics2D.Raycast((Vector2)transform.position, Vector2.left, 1.5f, LayerMask.GetMask("Map")).collider == null)
            {
                setV(-3, 1.5f);
            }
        }
    }

    void unlock()
    {
        busy = false;
    }

    protected override void OnZero()
    {
        base.OnZero();
        if (ev) { 
            ev.afterSth(0);
            ev = null;
        }
    }
}
