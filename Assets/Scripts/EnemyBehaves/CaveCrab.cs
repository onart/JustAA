using UnityEngine;

public class CaveCrab : Enemy
{
    readonly float ACTTIME = 0.5f / SysManager.difficulty;
    // Update is called once per frame
    public void actRestart()
    {
        if (actTime == 1000)
        {
            actTime = ACTTIME;
            CancelInvoke("Act");
            Invoke(nameof(Act), ACTTIME);
        }
    }

    protected override void Move()
    {
        switch (st)
        {
            case state.FREE:
                int dir = Random.Range(-1, 2);
                if (dir != 0) anim.SetBool("WALK", true);
                else anim.SetBool("WALK", false);
                setVX(dir);
                if (dir * transform.localScale.x > 0)
                {
                    FaceBack();
                }
                break;
            case state.HOST:
                Facing();
                FaceBack(); //기본 스프라이트가 왼쪽을 보게 만들어버린 경우 Facing 뒤에 이걸 붙이기
                float diff = p.position.x - transform.position.x;
                if (Mathf.Abs(diff) < 0.5f) {
                    anim.SetBool("WALK", false);
                    anim.SetTrigger("PINCH");
                    actTime = 1000;
                }
                else { 
                    setVX(Mathf.Clamp(diff, -2, 2)); 
                    anim.SetBool("WALK", true); 
                }
                break;
            default:
                break;
        }
    }

    protected override void St()
    {
        exp = 40;
        maxHp = 1 + 25 * SysManager.difficulty;
        hp = maxHp;
        at.face = 1;
        actTime = ACTTIME;
        rage = 5;
    }

    public override void GetHit(int delta, Vector2 force)
    {
        base.GetHit(delta, force);
        Facing();
        FaceBack();
    }
}
