using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEv: Entity
{
    //flg가 condition 이상 or 이하가 되면 오브젝트 파괴
    public BaseSet.Flags flg;
    public int condition;
    public bool over;   //참: 이상, 거짓: 미만

    private void Update()
    {
        if (over)
        {
            if (p.FLAGS[(int)flg] >= condition) Destroy(gameObject);
        }
        else
        {
            if (p.FLAGS[(int)flg] < condition) Destroy(gameObject);
        }
    }

    protected override void OnRecieve()
    {
    }

    public override void ObjAct()
    {
    }

    public override void St()
    {
    }

    public override void Up()
    {
    }
}
