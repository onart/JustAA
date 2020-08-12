using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    Player p;

    void Start()
    {
        p = gameObject.GetComponentInParent<Player>();
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        //if (!p.onground && (col.gameObject.layer == LayerMask.NameToLayer("Map") || col.gameObject.layer == LayerMask.NameToLayer("EnemyBody"))) {
        if (!p.onground) {
            p.onground = true;
            p.jumphold = 62;
            p.Hold();
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Map")) { 
            p.onground = false;            
        }
    }

}
