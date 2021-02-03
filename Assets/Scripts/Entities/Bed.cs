using UnityEngine;

public class Bed : Entity
{
    public override void ObjAct()
    {
        if (p.FLAGS[(int)BaseSet.Flags.MYBED] == 0)
        {
            tm.Dialog_Start(2, this);
            p.FLAGS[(int)BaseSet.Flags.MYBED] = 1;
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
        spacepos = new Vector3(0.8f, 1, 0);
        rayorigin = Vector2.zero;
        raydir = Vector2.right;
        raydistance = 1.1f;
    }
    protected override void OnRecieve()
    {
        //선택지와 무관한 개체이므로 공함수
    }
}
