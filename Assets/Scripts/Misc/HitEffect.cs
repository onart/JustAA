using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public GameObject hitPrefab;
    public Transform pt;
    public CircleCollider2D cl;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy") || col.gameObject.tag == "plat") {
            Vector2 v2 = cl.offset;
            v2.x *= pt.localScale.x;
            v2.y *= 0.3f;
            Instantiate(hitPrefab, transform.position + (Vector3)v2, Quaternion.identity);
        }
    }
}
