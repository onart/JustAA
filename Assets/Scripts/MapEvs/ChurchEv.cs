using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchEv : MapEv
{
    public override void Stt() {
        if (p.FLAGS[(int)BaseSet.Flags.STAGE1] < 3)
        {
            tm.Dialog_Start(26, this);
            p.FLAGS[(int)BaseSet.Flags.STAGE1] = 3;
        }
    }

}
