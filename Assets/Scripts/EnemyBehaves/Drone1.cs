﻿using UnityEngine;

public class Drone1 : Enemy
{
    public int sw;              //애니메이터에서 전달받을 현재 상태.
    int formerSw;               
    float alpha;                //체력이 0이 되면 점점 투명해지라고
    float y1, y2, y0;           //가만히 있을 때 자연스럽게 위아래로 움직임
    const float delayer = 0.5f;
    bool inRange;               //플레이어가 사정권에 있나?
    void Update()       //애니메이터 머신과의 연계(sw) 위주
    {
        
        if (sw == 0)    //이동 or 가만히 있을 때.
        {
            y2 += 0.01f;
            y1 = Mathf.Sin(Mathf.PI * y2) / 20;
            transform.position = new Vector2(transform.position.x, y0 + y1);
            if (detected)
            {
                if (p.position.x - transform.position.x < 1)
                {
                    inRange = true;
                }
                else
                {
                    inRange = false;
                }
                MoveTo();
            }
        }
        if (sw - formerSw == 1) 
        {
            Invoke("Rush", 0.5f);
        }
        formerSw = sw;
    }

    void Facing()
    {
        if (transform.localScale.x > 0 && p.position.x < transform.position.x) 
        {
            FaceBack();
        }
        else if(transform.localScale.x < 0 && p.position.x > transform.position.x)
        {
            FaceBack();
        }
    }

    protected override void St()
    {
        y0 = transform.position.y;
        maxHp = 20;
        hp = maxHp;
        alpha = 1;
        Behave();
        inRange = false;
        at.face = 1;
    }

    protected override void Behave()
    {
        if (!toDo.Equals("NIL"))
        {
            if (sw == 0 && inRange) 
            {
                int i = Random.Range(1, 5);
                if (i <= 2) anim.SetTrigger("PUNCH" + i);
            }
        }
        else { 

        }
        Invoke("Behave", delayer);
    }

    void Rush()
    {        
        CancelInvoke("Rush");
        rb2d.velocity = new Vector2(transform.localScale.x * 10, 0);       
    }

    protected override void OnZero()
    {
        alpha -= 0.02f;
        sr.color = new Color(1, 1, 1, alpha);
        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
        Invoke("OnZero", 0.02f);
    }

    void MoveTo()
    {
        if (!inRange)
        {
            rb2d.AddForce((p.position - transform.position) * Time.deltaTime * 60);
            Facing();
        }
        else
        {            
            Facing();
        }
    }
}
