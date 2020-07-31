using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 병진이동만을 하며, 하드에서는 속도가 빨라짐(급가속을 일정 쿨타임으로 사용)
// 현재 주인공 접촉에 의한 피격모션이 너무 짧음
public class Spark : Enemy
{
    public GameObject blast;    //폭발효과 프리팹
    public GameObject split;    //자신의 프리팹     

    bool cool;                  //하드 전용 돌진 쿨타임
    bool after = false;                 //분열 이후 폭발하지 않는 시간
    float red;

    void Update()       //애니메이터 머신과의 연계(sw) 위주
    {

    }

    protected override void Move()
    {
        if (st == state.FREE)
        {
            int dir = Random.Range(0, 3);
            switch (dir)
            {
                case 0:
                    setVX(-1);
                    if (transform.localScale.x < 0)
                    {
                        FaceBack();
                    }
                    break;
                case 1:
                    setVX(0);
                    break;
                case 2:
                    setVX(1);
                    if (transform.localScale.x > 0)
                    {
                        FaceBack();
                    }
                    break;
                default:
                    return;
            }
        }
        else if (st == state.HOST)
        {
            if (p.position.x < transform.position.x)
            {
                setVX(-2);
                if (transform.localScale.x < 0)
                {
                    FaceBack();
                }
            }
            else
            {
                setVX(2);
                if (transform.localScale.x > 0)
                {
                    FaceBack();
                }
            }
        }
    }

    protected override void OnZero()
    {
        st = state.SLEEP;        
        //1초 후 나머지 모두 비활성화하고 유폭
        sr.color = new Color(1, 1 - red, 1 - red);
        if (red < 1)
        {
            red += 0.1f;
            Invoke("OnZero", 0.1f);
            return;
        }
        var bls = Instantiate(blast);
        bls.transform.position = p.position;
        Destroy(gameObject);
    }

    protected override void St()
    {
        red = 0;
        cool = true;
        after = false;
        at.gameObject.SetActive(false);
        Invoke("prepare", 1.2f);
        if (exp == 0)
        {
            exp = 40;
            maxHp = (int)(16 * (SysManager.difficulty / 2.0f));
            hp = maxHp;
        }
        at.face = 1;
        actTime = 0.5f / SysManager.difficulty;
        rage = 1000;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (after && SysManager.difficulty == 2 && hp > 1)  //하드에 한해 플레이어에 닿는 즉시 폭발+분열, 분열 후 1.2초 내에는 닿아도 폭발하지 않음. 그 외에는 항상 몸 콜라이더를 공격으로 유지
        {
            if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
            {
                after = false;
                var bls = Instantiate(blast);
                bls.transform.position = transform.position;
                var sp1 = Instantiate(split);
                var sp2 = Instantiate(split);
                var ls = transform.localScale;
                sp1.transform.position = transform.position;
                sp1.transform.localScale = ls * 0.9f;
                sp2.transform.localScale = ls * 0.9f;
                sp1.GetComponent<Spark>().setSplit(exp / 2, hp / 2);
                sp2.GetComponent<Spark>().setSplit(exp / 2, hp / 2);

                Destroy(gameObject);
            }
        }
    }

    public void setSplit(int xp, int mHp) //right가 1이면 오른쪽, -1이면 왼쪽
    {
        exp = xp;
        maxHp = mHp;
        hp = mHp;
    }

    void prepare()
    {
        after = true;
        at.gameObject.SetActive(true);
    }
}
