using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapEv : Entity
{
    //맵에 형체 없이 배치되어 플레이어를 인식하고 오브젝트 접촉 없이도 행동에 따라 이벤트를 발생시키는 것을 위한 클래스다.
    //사물과의 상호작용 이외의 이벤트를 다룬다.
    protected override void OnRecieve()
    {
    }

    public override void ObjAct()
    {
    }

    public override void St()
    {
    }

    public override void Up()   //이거 안 쓰고 Update 오버라이드하자.
    {
    }

}
