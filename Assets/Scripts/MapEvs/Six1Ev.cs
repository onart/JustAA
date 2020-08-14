using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Six1Ev : MapEv
{
    public override void Stt()
    {
        if (p.FLAGS[(int)BaseSet.Flags.STAGE1] < 3) 
        {
            tm.Dialog_Start(27, this);
            p.FLAGS[(int)BaseSet.Flags.STAGE1] = 3;
        }
    }
}
