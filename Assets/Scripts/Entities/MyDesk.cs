using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDesk : Entity
{

    public override void ObjAct()
    {
        switch (p.FLAGS[(int)BaseSet.Flags.OUTEXP])
        {
            case 0:
                tm.Dialog_Start(18, this);
                return;
            case 3:
                tm.Dialog_Start(16, this);
                return;
            case 4:
                switch (p.FLAGS[(int)BaseSet.Flags.STAGE1])
                {
                    case 0:
                        tm.Dialog_Start(17, this);
                        return;
                    case 1:
                        return;
                }
                return;
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
