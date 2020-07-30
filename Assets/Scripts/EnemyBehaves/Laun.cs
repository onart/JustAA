using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laun : Enemy
{
    public GameObject arm1, arm2;
    float ang1, ang2;   //총알 발사각

    void Update()
    {

    }

    protected override void Move()
    {
        //좌우이동 및 발사. 하드는 정조준사격과 예측사격을 동시에
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

}
