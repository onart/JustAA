using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    protected int maxHp, hp;            //적의 체력. 적 역시 언젠가는 회복하지 않을까?라는 생각에 maxHp도 추가
    protected bool detected;            //플레이어 포착 시 행동 양식 달라짐
    protected Transform p;                 //플레이어 포착 시 그 위치를 파악하게 됨
    protected Rigidbody2D rb2d;
    protected Animator anim;
    public string toDo;              //Invoke를 이용하기 위한 것? 아직은 잘 모르겠음
    protected SpriteRenderer sr;

    public Attacker at;              //힘을 조절하기 위한 것
    public Collider2D detector;        //플레이어를 감지하면 이 트리거는 사라지면서 호전성을 띠게 됨
    private FoeHp fh;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fh = GetComponentInChildren<FoeHp>();
        detected = false;
        Sleep();        //기본이 자는 상태, 원하지 않는 경우는 St()에서 풀어라.
        St();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            detector.enabled = false;
            detected = true;
            toDo = "Idle";
            p = col.gameObject.GetComponent<Player>().transform;
        }
    }
    protected void HPChange(int delta)
    {
        hp += delta;
        if (hp > maxHp) hp = maxHp;
        else if (hp <= 0) { 
            hp = 0;
            toDo = "NIL";
            GetComponent<Collider2D>().enabled = false;
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
        anim.SetTrigger("HIT");
    }

    protected void Sleep()         //예를 들어 아주 멀리에 적이 있으면 일일이 행동할 게 아니라 재우는 게 이득
    {
        detector.enabled = true;
        toDo = "NIL";
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

    protected abstract void OnZero();       //체력 0일 때의 동작을 정의
    protected abstract void St();           //파생 클래스에서 Start에 더 들어갈 것을 정의
    protected abstract void Behave();       //파생 클래스의 행동 양식
}
