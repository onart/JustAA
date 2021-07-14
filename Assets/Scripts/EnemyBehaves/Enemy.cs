using System.Collections;
using UnityEngine;

public abstract class Enemy : BaseHzd
{
    protected enum state { SLEEP, FREE, HOST };
    /*
     * 휴면 : update을 포함하여 아무것도 하지 않음. 카메라 안에 들어오면 자유 모드로
     * 자유 : 자연스러운 움직임을 하도록 노력함. 카메라 밖으로 나가면 휴면 모드로, 트리거를 건드리면 적대 모드로(cancelInvoke)
     * 적대 : 플레이어를 공격하도록 노력함. 트리거 밖으로 나가서 rage 시간이 지나면 자유 모드로(Invoke)
    */
    protected state st = state.SLEEP;
    protected float rage;               //파생 클래스에서 반드시 지정하도록 하자

    public Collider2D detector;         //상태 변화의 기준 범위 2
    private FoeHp fh;
    protected float actTime;

    float alpha;                          //자신의 투명도

    public int sw;                        //유사인터럽트용 스위치. 0인 경우 파생 클래스에 관계 없이 이동 중이거나 가만히 있는 중, 애니메이터에서 값 전달받음. 그 외에는 파생별로 다름

    // Start is called before the first frame update
    protected override void St()
    {
        if (!DataFiller.load_complete) {
            Invoke(nameof(St), 0.03f);
            return;
        };
        if (!fh) fh = GetComponentInChildren<FoeHp>();
        alpha = 1;
    }

    protected void Act()    //이동담당
    {
        Move();
        Invoke("Act", actTime);
    }

    private void OnTriggerEnter2D(Collider2D col)       //상태머신 전이기
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CancelInvoke("setFree");
            if (st == state.SLEEP)
            {
                Act();
            }
            st = state.HOST;
            if (p == null)
            {
                p = col.gameObject.GetComponent<Player>().transform;
                prb2d = col.gameObject.GetComponent<Rigidbody2D>();
            }
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
        if (st == state.SLEEP)
        {
            st = state.FREE;
            Act();
        }
    }

    private void OnBecameInvisible()                    //상태머신 전이기
    {
        if (st == state.FREE)
        {
            st = state.SLEEP;
            CancelInvoke("Act");
        }
    }

    public override void GetHit(int delta, Vector2 force)
    {
        HPChange(-delta);
        rb2d.AddForce(force);
        fh.gameObject.SetActive(true);
        fh.transform.localScale = new Vector2((float)hp / maxHp / 2, 0.5f);
        fh.alphaTime = 60;
        if (sw == 0) { anim.SetTrigger("HIT"); }
    }

    private void setFree()
    {
        st = state.FREE;
        if (sr.isVisible) st = state.SLEEP;
    }

    protected override IEnumerator OnZero()
    {
        GetComponent<Collider2D>().enabled = false;
        Player.inst.GainExp(exp);
        at.enabled = false;
        st = state.SLEEP;
        Destroy(at);
        while (alpha > 0)
        {
            alpha -= 0.02f;
            sr.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }
    protected abstract void Move();         //파생 클래스에서 이동 판단에 대한 정의
}
