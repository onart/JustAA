using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laun : Enemy
{
    public GameObject arm1, arm2;
    public GameObject bullet;
    float ang1, ang2;   //총알 발사각. 하드에 한해 2번 팔이 예측샷을 날림. 그 외의 경우 항상 실시간 정조준

    const float bulletSpeed = 2.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            snipe1();
            pred_shoot2();
            fire();
        }
    }

    protected override void Move()
    {
        //좌우이동(거리를 두려는 경향성) 및 발사. 하드는 정조준사격과 예측사격을 동시에
        //현재 사격 각도 이상함. 팔 각도는 정상
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

    void setAngle1(float ang)   //팔 하나 각도
    {        
        arm1.transform.rotation = Quaternion.AngleAxis(-ang - 34.5f, Vector3.forward);
        ang1 = ang;
    }

    void setAngle2(float ang)   //팔 둘 각도
    {
        arm2.transform.rotation = Quaternion.AngleAxis(-ang - 34.5f, Vector3.forward);
        ang2 = ang;
    }

    void snipe1()   //팔 1 정조준사격
    {
        float dx = Mathf.Abs(p.position.x - transform.position.x);
        float dy = p.position.y - transform.position.y;
        setAngle1(Mathf.Atan(dy / dx) * Mathf.Rad2Deg);
    }

    void snipe2()   //팔 2 정조준사격
    {
        float dx = Mathf.Abs(p.position.x - transform.position.x);
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
        dy += late * prb2d.velocity.y;
        setAngle2(Mathf.Atan(Mathf.Abs(dy / dx)) * Mathf.Rad2Deg);
    }

    private void fire()
    {
        var b1 = Instantiate(bullet, transform.position, arm1.transform.rotation);
        var b2 = Instantiate(bullet, transform.position, arm2.transform.rotation);
    }
}
