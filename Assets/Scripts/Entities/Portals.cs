using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public Transform targetPos;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.gameObject.transform.position = targetPos.position;
            var g = col.gameObject.GetComponent<Rigidbody2D>();
            g.velocity = new Vector2();
            var p = col.gameObject.GetComponent<Player>();            
            p.HpChange(-1 * (10 << SysManager.difficulty));
        }
    }
}
