using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laun : Enemy
{
    public GameObject arm1, arm2;
    public GameObject bullet;
    float ang1, ang2;   //총알 발사각. 하드에 한해 2번 팔이 예측샷을 날림. 그 외의 경우 항상 실시간 정조준
                        //아종을 만들어 벽 타기 가능하게?

    public static readonly float bulletSpeed = 6.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            snipe1();
            if (SysManager.difficulty == 3) pred_shoot2();
            else snipe2();
            fire();
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
                setVX(1);
                if (transform.localScale.x < 0)
                {
                    FaceBack();
                }
            }
            else
            {
                setVX(-1);
                if (transform.localScale.x > 0)
                {
                    FaceBack();
                }
            }
            if (sw == 0)
            {
                int dir = Random.Range(0, 7 - SysManager.difficulty);
                switch (dir)
                {
                    case 0:
                        snipe1();
                        if (SysManager.difficulty == 3) pred_shoot2();
                        else snipe2();
                        fire();
                        break;
                    default:
                        return;
                }
            }
        }
    }

    protected override void St()
    {
        exp = 60;
        maxHp = (int)(10 * (SysManager.difficulty / 2.0f)) + 20;
        hp = maxHp;
        at.face = 1;
        actTime = 0.2f / SysManager.difficulty;
        rage = 1000;
    }

    void setAngle1(float ang)   //팔 하나 각도
    {
        ang += 5;
        if (transform.localScale.x > 0) { 
            ang += 170;
            arm1.transform.localRotation = Quaternion.AngleAxis(180 + ang - 34.5f, Vector3.forward);
        }
        else
        {
            arm1.transform.localRotation = Quaternion.AngleAxis(-ang - 34.5f, Vector3.forward);
        }
        ang1 = ang;
    }

    void setAngle2(float ang)   //팔 둘 각도
    {
        ang += 5;
        if (transform.localScale.x > 0)
        {
            ang += 170;
            arm2.transform.localRotation = Quaternion.AngleAxis(180 + ang - 34.5f, Vector3.forward);
        }
        else
        {
            arm2.transform.localRotation = Quaternion.AngleAxis(-ang - 34.5f, Vector3.forward);
        }
        ang2 = ang;
    }

    void snipe1()   //팔 1 정조준사격
    {
        float dx = p.position.x - transform.position.x;
        float dy = p.position.y - transform.position.y;
        setAngle1(Mathf.Atan(dy / dx) * Mathf.Rad2Deg);
    }

    void snipe2()   //팔 2 정조준사격
    {
        float dx = p.position.x - transform.position.x;
        float dy = p.position.y - transform.position.y;
        setAngle2(Mathf.Atan(dy / dx) * Mathf.Rad2Deg);
    }

    void pred_shoot2()  //팔 2 예측사격. 자신은 정지 상태로 가정하며 속도가 있는 경우는 나중에 따로 고려할 것
    {
        float dx = p.position.x - transform.position.x;
        float dy = p.position.y - transform.position.y;
        float dist = Mathf.Sqrt(dx * dx + dy * dy);

        float late = dist / bulletSpeed;
        dx += late * prb2d.velocity.x;
        dy += late * prb2d.velocity.y / 2;
        setAngle2(Mathf.Atan(Mathf.Abs(dy / dx)) * Mathf.Rad2Deg);
    }

    private void fire()
    {
        Instantiate(bullet, transform.position + Vector3.left * 0.1f, Quaternion.AngleAxis(ang1, Vector3.forward));
        Instantiate(bullet, transform.position, Quaternion.AngleAxis(ang2, Vector3.forward));
    }
}
