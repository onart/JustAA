using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//하나의 말만 하기 위한 객체는 이것을 붙임(ex:설명자)
public class Parrot : Entity
{
    public int d_no;
    public override void ObjAct()
    {
        tm.Dialog_Start(d_no, this);
    }

    public override void St()
    {
    }

    public override void Up()
    {
    }

    protected override void OnRecieve()
    {
    }
}
