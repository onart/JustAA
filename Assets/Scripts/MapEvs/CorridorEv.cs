using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorEv : MapEv         //Corridor 맵의 비사물 이벤트를 정의
{
    public Door dr1, dr2;
    public GameObject mob;  //등장하는 적을 말하는 것이다.
    protected override void Stt()   //St는 맵 입장과 동시에 발생시킬 이벤트를 정의함.
    {
        if (p.FLAGS[(int)BaseSet.Flags.OUTEXP] == 0)
        {
            mob = Resources.Load<GameObject>("Prefabs/DRONE1");
            mob = Instantiate(mob);
            mob.transform.position = new Vector2(13.34f, -4.6f);
            p.FLAGS[(int)BaseSet.Flags.OUTEXP] = 1;
            dr1.setResponse(6);
            dr2.setResponse(6);
            tm.Dialog_Start(5,this);
        }
    }

    void Update()
    {
        if (p.FLAGS[(int)BaseSet.Flags.OUTEXP] >= 3)
        {
            if (p.FLAGS[(int)BaseSet.Flags.STAGE1] == 0)
            {
                dr2.setResponse(21);
            }
            gameObject.SetActive(false);
        }
        else if (mob == null) 
        {
            p.FLAGS[(int)BaseSet.Flags.OUTEXP] = 2;
        }
        if (p.FLAGS[(int)BaseSet.Flags.OUTEXP] == 2)    //1회에 한해 나타나는 괴물을 없애면 저 플래그는 2가 될 것이다. 거기서 처리하지 않는 이유는 문을 다시 열기 위해서다.
        {
            p.FLAGS[(int)BaseSet.Flags.OUTEXP] = 3;
            dr1.setResponse(-1);
            tm.Dialog_Start(7, this);
        }
    }
}
