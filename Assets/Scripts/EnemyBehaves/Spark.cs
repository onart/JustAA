using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : Enemy
{
    public GameObject blast;    //폭발효과 프리팹
    public GameObject split;    //자신의 프리팹
    // 병진이동만을 하며, 하드에서는 속도가 빨라짐(급가속을 일정 쿨타임으로 사용)
    // 현재 주인공 접촉에 의한 피격모션이 너무 짧음

    void Update()
    {
        
    }

    protected override void Move()
    {
        
    }

    protected override void OnZero()
    {
        //나머지 모두 비활성화하고 유폭
        var bls = Instantiate(blast);
        bls.transform.position = transform.position;
        Destroy(gameObject);
    }

    protected override void St()
    {
        exp = 40;
        maxHp = (int)(40 * (SysManager.difficulty / 2.0f));
        hp = maxHp;
        at.face = 1;
        actTime = 0.5f / SysManager.difficulty;
        rage = 1000;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (SysManager.difficulty == 3) //하드에 한해 플레이어에 닿는 즉시 폭발+분열. 그 외에는 항상 몸 콜라이더를 공격으로 유지
        {
            if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
            {
                var sp1 = Instantiate(split);
                var sp2 = Instantiate(split);
                sp1.GetComponent<Spark>().setSplit(exp / 2, hp);
                sp2.GetComponent<Spark>().setSplit(exp / 2, hp);
                OnZero();
            }
        }
    }

    void setSplit(int xp, int mHp)
    {
        exp = xp;
        maxHp = mHp;
        hp = mHp;
    }
}
