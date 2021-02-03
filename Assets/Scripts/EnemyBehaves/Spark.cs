using UnityEngine;


// 병진이동만을 하며, 하드에서는 속도가 빨라짐(급가속을 일정 쿨타임으로 사용)
// 현재 주인공 접촉에 의한 피격모션이 너무 짧음
public class Spark : Enemy
{
    public GameObject blast;    //폭발효과 프리팹 

    bool cool;                  //하드 전용 돌진 쿨타임
    float red;
    bool youpok = false;        //유폭 모드

    void Update()       //애니메이터 머신과의 연계(sw) 위주
    {
        if (youpok)
        {
            rb2d.velocity = new Vector2(p.position.x - transform.position.x, 0) * (1 << SysManager.difficulty);
        }
    }

    protected override void Move()
    {
        if (st == state.FREE)
        {
            int dir = Random.Range(0, 3);
            switch (dir)
            {
                case 0:
                    setVX(-2);
                    if (transform.localScale.x < 0)
                    {
                        FaceBack();
                    }
                    break;
                case 1:
                    setVX(0);
                    break;
                case 2:
                    setVX(2);
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
                setVX(-3);
                if (transform.localScale.x < 0)
                {
                    FaceBack();
                }
            }
            else
            {
                setVX(3);
                if (transform.localScale.x > 0)
                {
                    FaceBack();
                }
            }
        }
    }

    protected override void OnZero()
    {
        youpok = true;
        st = state.SLEEP;
        //2초 후 나머지 모두 비활성화하고 유폭
        sr.color = new Color(1, 1 - red, 1 - red);
        if (red < 1)
        {
            red += 0.1f;
            Invoke("OnZero", 0.2f);
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
        if (exp == 0)
        {
            exp = 40;
            maxHp = (int)(16 * (SysManager.difficulty / 2.0f));
            hp = maxHp;
        }
        at.face = 1;
        actTime = 0.5f / SysManager.difficulty;
        rage = 10;
    }

    public void setSplit(int xp, int mHp) //right가 1이면 오른쪽, -1이면 왼쪽
    {
        exp = xp;
        maxHp = mHp;
        hp = mHp;
    }
}
