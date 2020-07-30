using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sting : Enemy
{
    // Update is called once per frame
    void Update()
    {
        
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

    protected override void Move()
    {
        //평소의 움직임/포착시의 움직임/쿨타임(1패턴)
    }
}
