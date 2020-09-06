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
        //판단부(lr, attk를 조절하는 부분)
        if (!busy) decide();
        //조작부(player에서처럼 함수만으로 이루어지는데, 함수만으로 이루어지도록 하자.)
        lrMove();
        atSet();       
        //틱 처리부
        tick++;
        if (tick >= 50) tick = 0;   //틱 제한은 각 쿨타임 틱수의 공배수
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

    void throw1()   //투척 행동: 스나이핑
    {

    }
    
    void throw2()   //투척 행동: 예측샷
    {

    }

    void chase()    //투척 회수. 레이캐스트를 잘 이용하자.
    {

    }

    void lrMove()   //속도 지정, 
    {
        setVX(lr*speed);
    }

    void atSet()    //공격 지정
    {
        switch (attk)   //0: 입력없음
        {
            default:
                return;
        }
    }

    void decide()
    {
        float distance = p.position.x - transform.position.x;
        if (distance > 4 || distance < -4)
        {
            lr = 0;
            attk = 2;   //던지기 페이즈, 이후 자동으로 회수단계까지 넘어감
            return;
        }
        else if (distance > 1)
        {
            if (p.position.x - transform.position.x > 0) lr = 1;
            else lr = -1;
            //따라감. 하드에서 더 빠름
        }
        else
        {
            //하드에서는 근접공격을 할 때 느리게 이동. 그 아래에서는 이동 없음. 보통 이상에서는 공격 거리 1.5타일, 쉬움에서는 공격 거리 1타일
            attk = 1;
        }
    }

}
