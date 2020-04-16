using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDesk : Entity
{

    public override void ObjAct()
    {
        if (p.FLAGS[(int)BaseSet.Flags.MYDESK] == 0) {
            tm.Dialog_Start(2, this);
            p.FLAGS[(int)BaseSet.Flags.MYDESK] = 1;
        }
        else
        {
            tm.Dialog_Start(3, this);
            p.HpChange(p.MHP);
        }
    }

    public override void Up()   //업데이트 함수.
    {

    }

    public override void St()
    {
        spacepos = new Vector3(0, 1, 0);
        rayorigin = new Vector2(-0.4f, 0);
        raydir = Vector2.right;
        raydistance = 0.8f;
    }
    protected override void OnRecieve()
    {
        //선택지와 무관한 개체이므로 공함수
    }
}
