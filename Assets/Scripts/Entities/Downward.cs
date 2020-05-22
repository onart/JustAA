using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downward : Entity
{
    Door dr;

    protected override void OnRecieve()
    {
        if ((dialog == 28 || dialog == 29) && selection == 0) 
        {
            //10층 복도로 이동
            p.FLAGS[(int)BaseSet.Flags.STAGE1] = 4;
            dr.dynamicUse("10Corridor","virtual");
        }
    }

    public override void St()
    {
        spacepos = new Vector3(-0.2f, 1.3f, 0);
        rayorigin = Vector2.zero;
        raydir = Vector2.up;
        raydistance = 1;
        dr = FindObjectOfType<Door>();
    }

    public override void Up()
    {
    }

    public override void ObjAct()
    {
        if (p.FLAGS[(int)BaseSet.Flags.STAGE1] >= 4)
        {
            tm.Dialog_Start(29, this);
        }
        else if (p.HasItem(0))   //매트리스가 있음
        {
            tm.Dialog_Start(28, this);
        }
        else
        {
            tm.Dialog_Start(27, this);
        }
    }
}
