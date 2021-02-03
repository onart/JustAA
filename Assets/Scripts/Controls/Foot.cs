using UnityEngine;

public class Foot : MonoBehaviour
{
    Player p;
    CircleCollider2D foot;
    int footMask = 0;

    void Start()
    {
        p = GetComponentInParent<Player>();
        foot = GetComponent<CircleCollider2D>();
        footMask += 1 << LayerMask.NameToLayer("Map");
        footMask += 1 << LayerMask.NameToLayer("Enemy");
        footMask += 1 << LayerMask.NameToLayer("Foreground");
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        //if (!p.onground && (col.gameObject.layer == LayerMask.NameToLayer("Map") || col.gameObject.layer == LayerMask.NameToLayer("EnemyBody"))) {
        if (!p.onground && foot.IsTouchingLayers(footMask))
        {
            p.onground = true;
            p.jumphold = 62;
            p.Hold();
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Map") || col.gameObject.layer == LayerMask.NameToLayer("Foreground") || col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            p.onground = false;
        }
    }

}
