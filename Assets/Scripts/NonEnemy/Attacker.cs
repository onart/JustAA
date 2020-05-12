using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : HPChanger
{
    public Vector2 force;
    public int face = 1;    //좌측을 본다/우측을 본다.
    //이것(Attacker 객체)을 가지고 있는 물체가 업데이트 함수를 통해 force를 경우에 따라 조정할 것.
    //예를 들면 왼쪽으로 대포알을 날리면 force는 왼쪽을 향함. 물론 이건 단순 예고, 대포알 같은 건 force를 0으로 하고 콜라이더로 하는 게 이로움. 주먹을 날릴 때 등에 활용
    public int down;  //강공격(눕히는) 판정인지 지정

    protected override void Act(Player p)
    {
        if (p == null) return;
        else
        {
            p.GetHit(delta, down);
            force.x *= face;
            p.rb2d.AddForce(force);
        }
    }
}
