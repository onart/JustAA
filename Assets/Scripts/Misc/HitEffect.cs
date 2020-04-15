using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public GameObject hitPrefab;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            GameObject hitFX = Instantiate(hitPrefab, col.transform.position, Quaternion.identity);
        }
    }
}
