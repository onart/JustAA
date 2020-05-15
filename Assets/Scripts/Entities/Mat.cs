using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat : Entity
{
    protected override void OnRecieve()
    {
    }

    public override void St()
    {
        spacepos = new Vector3(-0.2f, 1.3f, 0);
        rayorigin = new Vector2(-0.5f, 0);
        raydir = Vector2.right;
        raydistance = 1;
    }

    public override void Up()
    {
        if (p.HasItem(0))   //매트리스가 있음
        {            
            Destroy(gameObject);
        }
    }

    public override void ObjAct()
    {
        if (p.FLAGS[(int)BaseSet.Flags.STAGE1] < 3)
        {
            tm.Dialog_Start(29, this);
            p.FLAGS[(int)BaseSet.Flags.KEYS]++;
        }
        else if(p.FLAGS[(int)BaseSet.Flags.STAGE1] == 3){
            tm.Dialog_Start(30, this);
            p.FLAGS[(int)BaseSet.Flags.KEYS]++;
        }
    }
}
