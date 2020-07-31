using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected enum state {SLEEP, FREE, HOST};
    /*
     * 휴면 : update을 포함하여 아무것도 하지 않음. 카메라 안에 들어오면 자유 모드로
     * 자유 : 자연스러운 움직임을 하도록 노력함. 카메라 밖으로 나가면 휴면 모드로, 트리거를 건드리면 적대 모드로(cancelInvoke)
     * 적대 : 플레이어를 공격하도록 노력함. 트리거 밖으로 나가서 rage 시간이 지나면 자유 모드로(Invoke)
    */
    protected state st = state.SLEEP;
    protected float rage;               //파생 클래스에서 반드시 지정하도록 하자

    protected int maxHp, hp, exp;       //적의 체력. 적 역시 언젠가는 회복하지 않을까?라는 생각에 maxHp도 추가, exp는 쓰러뜨리면 주는 경험치(재화)
    protected Transform p;              //플레이어 포착 시 그 위치를 파악하게 됨
    protected Rigidbody2D rb2d;
    protected Animator anim;
    protected SpriteRenderer sr;

    public Attacker at;                 //힘을 조절하기 위한 것
    public Collider2D detector;         //상태 변화의 기준 범위 2
    private FoeHp fh;
    protected float actTime;

    private static GameObject dmgTxt;     //맞을 때 출력할 텍스트 prefab
    private GameObject dmgTxtInst;        //dmgTxt의 인스턴스
    public DmgOrHeal doH;                 //텍스트 내용 설정자
    float alpha;                          //자신의 투명도

    public int sw;                        //유사인터럽트용 스위치. 0인 경우 파생 클래스에 관계 없이 이동 중이거나 가만히 있는 중, 애니메이터에서 값 전달받음. 그 외에는 파생별로 다름

    // Start is called before the first frame update
    void Start()
    {
        if (dmgTxt == null)
        {
            dmgTxt = Resources.Load<GameObject>("Prefabs/dmgTxt");
        }
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fh = GetComponentInChildren<FoeHp>();
        alpha = 1;
        St();        
    }

    protected void Act()
    {
        Move();
        Invoke("Act", actTime);
    }

    private void OnTriggerEnter2D(Collider2D col)       //상태머신 전이기
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CancelInvoke("setFree");            
            st = state.HOST;
            if (p == null) p = col.gameObject.GetComponent<Player>().transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)        //상태머신 전이기
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Invoke("setFree", rage);
        }
    }

    private void OnBecameVisible()                      //상태머신 전이기
    {
        if (st == state.SLEEP) {
            st = state.FREE;
            Act();
        }
    }

    private void OnBecameInvisible()                    //상태머신 전이기
    {
        if (st == state.FREE) {
            st = state.SLEEP;
            CancelInvoke("Act");
        }
    }

    protected void HPChange(int delta)
    {
        if (delta == 0) return;
        dmgTxtInst = Instantiate(dmgTxt);
        dmgTxtInst.transform.position = transform.position;
        doH = dmgTxtInst.GetComponent<DmgOrHeal>();
        if (delta < 0)
        {
            doH.SetText((-delta).ToString(), Color.white);
        }
        else
        {
            doH.SetText(delta.ToString(), Color.cyan);
        }
        hp += delta;
        if (hp > maxHp) hp = maxHp;
        else if (hp <= 0) { 
            hp = 0;
            GetComponent<Collider2D>().enabled = false;
            p.gameObject.GetComponent<Player>().GainExp(exp);
            at.enabled = false;
            OnZero();
        }
    }
    
    public void GetHit(int delta, Vector2 force)
    {
        HPChange(-delta);
        rb2d.AddForce(force);
        fh.gameObject.SetActive(true);
        fh.transform.localScale = new Vector2((float)hp / maxHp / 2, 0.5f);
        fh.alphaTime = 60;
        if (sw == 0) anim.SetTrigger("HIT");
    }

    private void setFree()
    {
        st = state.FREE;
        if (sr.isVisible) st = state.SLEEP;
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

    protected virtual void OnZero() {    //체력 0일 때의 동작을 정의
        st = state.SLEEP;
        Destroy(at);
        alpha -= 0.02f;
        sr.color = new Color(1, 1, 1, alpha);
        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
        Invoke("OnZero", 0.02f);
    }

    protected abstract void St();           //파생 클래스에서 Start에 더 들어갈 것을 정의
    protected abstract void Move();         //파생 클래스에서 이동 판단에 대한 정의
}
