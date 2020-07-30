using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone2 : Enemy
{
    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Move()
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
}
