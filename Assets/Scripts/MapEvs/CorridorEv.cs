using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorEv : MapEv         //Corridor 맵의 비사물 이벤트를 정의
{
    public Door dr;    //이 맵에 딱 하나 있는 문
    public GameObject mob;  //등장하는 적을 말하는 것이다.
    public override void St()   //St는 맵 입장과 동시에 발생시킬 이벤트를 정의함.
    {
        if (p.FLAGS[(int)BaseSet.Flags.OUTEXP] == 0)
        {
            mob = Resources.Load<GameObject>("Prefabs/DRONE1");
            mob = Instantiate(mob);
            p.FLAGS[(int)BaseSet.Flags.OUTEXP] = 1;
            dr.mode = false;
            tm.Dialog_Start(5,this);
        }
    }

    void Update()      //오버라이딩된 업데이트는 공함수. 최적화 기법이다
    {
        if (p.FLAGS[(int)BaseSet.Flags.OUTEXP] == 3)
        {
            gameObject.SetActive(false);
        }
        else if (mob == null) 
        {
            p.FLAGS[(int)BaseSet.Flags.OUTEXP] = 2;     //이건 잡몹을 만들기 전에 이벤트를 넘기기 위한 임시 코드다.
        }
        if (p.FLAGS[(int)BaseSet.Flags.OUTEXP] == 2)    //1회에 한해 나타나는 괴물을 없애면 저 플래그는 2가 될 것이다. 거기서 처리하지 않는 이유는 문을 다시 열기 위해서다.
        {
            p.FLAGS[(int)BaseSet.Flags.OUTEXP] = 3;
            dr.mode = true;
            tm.Dialog_Start(7, this);
        }
    }
}
