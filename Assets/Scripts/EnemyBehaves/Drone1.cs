using UnityEngine;

public class Drone1 : Enemy
{
    float y1, y2, y0;           //가만히 있을 때 자연스럽게 위아래로 움직임
    bool cool;                  //쿨타임.

    void Update()       //애니메이터 머신과의 연계(sw) 위주
    {
        if (st == state.SLEEP)
        {

        }
        else
        {
            if (sw == 0)    //이동 or 가만히 있을 때.
            {
                y2 += 0.01f;
                y1 = Mathf.Sin(Mathf.PI * y2) / 20;
                transform.position = new Vector2(transform.position.x, y0 + y1);
                if (st == state.HOST)
                {
                    if (cool && Mathf.Abs(p.position.x - transform.position.x) < 1)
                    {
                        Facing();
                        CancelInvoke("Act");
                        anim.SetTrigger("PUNCH" + Random.Range(1, 3));
                        cool = false;
                        Invoke("coolDown", 2.0f / SysManager.difficulty);
                        setVX(0);
                    }
                    else if (cool)
                    {
                        Act();
                    }
                }
            }
        }
    }

    void coolDown()
    {
        cool = true;
    }

    protected override void St()
    {
        base.St();
        exp = 40;
        y0 = transform.position.y;
        maxHp = 10 + 10 * SysManager.difficulty;
        hp = maxHp;
        at.face = 1;
        actTime = 0.5f / SysManager.difficulty;
        rage = 1000;
        cool = true;
    }

    public void Rush()
    {
        rb2d.velocity = new Vector2(transform.localScale.x * 10, 0);
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
                    if (transform.localScale.x > 0)
                    {
                        FaceBack();
                    }
                    break;
                case 1:
                    setVX(0);
                    break;
                case 2:
                    setVX(1);
                    if (transform.localScale.x < 0)
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
                setVX(-1);
                if (transform.localScale.x > 0)
                {
                    FaceBack();
                }
            }
            else
            {
                setVX(1);
                if (transform.localScale.x < 0)
                {
                    FaceBack();
                }
            }
        }
    }

}
