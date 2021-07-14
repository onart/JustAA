using UnityEngine;

public class Portals : HPChanger
{
    public Transform targetPos;

    protected override void Act()
    {
        Player.inst.transform.position = targetPos.position;
        var g = Player.inst.gameObject.GetComponent<Rigidbody2D>();
        g.velocity = Vector2.zero;
        var dam = delta << SysManager.difficulty;
        Player.inst.HpChange(-dam);
        Player.inst.reserveVx(0);
        Player.inst.reserveVy(0);
    }
}
