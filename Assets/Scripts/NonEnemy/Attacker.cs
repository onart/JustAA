using UnityEngine;

public class Attacker : HPChanger
{
    public enum Judg { 약 = 0, 중, 강 }; // 약: 피격모션 없음, 중: 피격모션만 있음, 강: 쓰러짐
    public Vector2 force;
    public int face = 1;    //좌측을 본다/우측을 본다.
    //이것(Attacker 객체)을 가지고 있는 물체가 업데이트 함수를 통해 force를 경우에 따라 조정할 것.
    //예를 들면 왼쪽으로 대포알을 날리면 force는 왼쪽을 향함. 물론 이건 단순 예고, 대포알 같은 건 force를 0으로 하고 콜라이더로 하는 게 이로움. 주먹을 날릴 때 등에 활용
    public Judg down;  //강공격(눕히는) 판정인지 지정

    protected override void Act()
    {
        if (p == null) return;
        else
        {
            p.GetHit(delta, (int)down);
            force.x *= face;
            p.rb2d.AddForce(force);
        }
    }
}
