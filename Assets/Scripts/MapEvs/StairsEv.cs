using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsEv : MapEv
{
    protected override void Stt()
    {
        if (p.FLAGS[(int)BaseSet.Flags.STAGE1] == 1)
        {
            p.FLAGS[(int)BaseSet.Flags.STAGE1] = 2;
            tm.Dialog_Start(23,this);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        tm.Dialog_Start(22, this);
    }
}
