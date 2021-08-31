using System.Collections;
using UnityEngine;

public abstract class BaseHzd : MonoBehaviour
{
    static protected Transform p;       //플레이어 위치
    static protected Rigidbody2D prb2d;        //플레이어의 2d강체

    public Attacker at;

    protected int maxHp, hp, exp;       //적의 체력. 적 역시 언젠가는 회복하지 않을까?라는 생각에 maxHp도 추가, exp는 쓰러뜨리면 주는 경험치(재화)
    protected Animator anim;
    protected SpriteRenderer sr;
    protected Rigidbody2D rb2d;         //자신의 2d강체
    protected Transform dmgPos;
    protected float dx, dy, relDeg;

    public int restHp
    {
        get
        {
            return hp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (p == null)
        {
            p = Player.inst.transform;
            prb2d = Player.inst.rb2d;
        }
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        dmgPos = transform;
        St();
    }

    protected void setVX(float x)
    {
        rb2d.velocity = new Vector2(x, rb2d.velocity.y);
    }

    protected void setVY(float y)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, y);
    }

    protected void setV(float x, float y)
    {
        rb2d.velocity = new Vector2(x, y);
    }

    protected void FaceBack()     //뒤를 돎.
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        if (transform.localScale.x > 0)
        {
            at.face = 1;
        }
        else
        {
            at.face = -1;
        }
    }
    protected void Facing()   //플레이어 바라보기. 기본 스프라이트가 오른쪽 보고 있는 기준.
    {
        if (transform.localScale.x > 0 && p.position.x < transform.position.x)
        {
            FaceBack();
        }
        else if (transform.localScale.x < 0 && p.position.x > transform.position.x)
        {
            FaceBack();
        }
    }

    protected virtual void HPChange(int delta)
    {
        if (delta == 0) return;
        var dmgTxtInst = Instantiate(BaseSet.dmgTxt);
        dmgTxtInst.transform.position = dmgPos.position;
        if (delta < 0)
        {
            dmgTxtInst.GetComponent<DmgOrHeal>().SetText((-delta).ToString(), Color.white);
        }
        else
        {
            dmgTxtInst.GetComponent<DmgOrHeal>().SetText(delta.ToString(), Color.cyan);
        }
        hp += delta;
        if (hp > maxHp) hp = maxHp;
        else if (hp <= 0)
        {
            hp = 0;
            StartCoroutine(OnZero());
        }
    }

    protected void Seek(Transform t = null)
    {
        if (t == null) t = transform;
        dx = p.position.x - t.position.x;
        dy = p.position.y - t.position.y + 0.5f;
        relDeg = Mathf.Atan(dy / dx) * Mathf.Rad2Deg;
    }

    public abstract void GetHit(int delta, Vector2 force);
    protected abstract IEnumerator OnZero();
    protected abstract void St();
}
