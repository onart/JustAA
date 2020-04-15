using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : HPChanger
{
    protected override void Act(Player p)
    {
        p.HpChange(delta);
        Destroy(gameObject);    //이 줄은 사라질 수도 있음.
    }
}
