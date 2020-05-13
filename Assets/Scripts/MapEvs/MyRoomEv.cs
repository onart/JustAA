using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomEv : MapEv
{
    public override void Stt()
    {
        Invoke("HowTo", 0.05f);
    }

    void HowTo()
    {
        if (p.FLAGS[(int)BaseSet.Flags.TUTORIAL] == 0)
        {
            p.FLAGS[(int)BaseSet.Flags.TUTORIAL] = 1;
            tm.Dialog_Start(19, this);
        }
    }
}
