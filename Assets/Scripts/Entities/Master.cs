using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : Entity
{

    protected override void OnRecieve()
    {
        
    }

    public override void ObjAct()
    {
        if (p.transform.position.x < transform.position.x) { 
            transform.localScale = new Vector2(-0.3f, 0.3f);
            p.transform.localScale = new Vector2(0.3f, 0.3f);
        }
        else { 
            transform.localScale = new Vector2(0.3f, 0.3f);
            p.transform.localScale = new Vector2(-0.3f, 0.3f);
        }
        tm.Dialog_Start(4);
    }

    public override void St()
    {
        if (p.FLAGS[(int)BaseSet.Flags.OUTEXP] < 3) { gameObject.SetActive(false); }    //관장 출현 조건 : 문 밖으로 나가서 몹을 잡고 들어옴
        spacepos = new Vector3(0, 1.3f, 0);
        rayorigin = new Vector3(-0.6f, 0.5f, 0);
        raydir = Vector2.right;
        raydistance = 1.2f;
    }

    public override void Up()
    {
        
    }
}
