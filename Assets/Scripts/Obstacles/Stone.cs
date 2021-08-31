using System.Collections;
using UnityEngine;

public class Stone : Attacker
{

    int StoneMask = 0;
    public Collider2D poly;
    public CircleCollider2D trig;
    int p_layer;

    private void Start()
    {
        p_layer = LayerMask.NameToLayer("Player");
        StoneMask += 1 << LayerMask.NameToLayer("Map");
        StoneMask += 1 << LayerMask.NameToLayer("Enemy");
        StoneMask += 1 << LayerMask.NameToLayer("Foreground");
        StoneMask += 1 << LayerMask.NameToLayer("Player");
    }

    private new void OnCollisionEnter2D(Collision2D col)
    {
        if (poly.IsTouchingLayers(StoneMask))
        {
            if (col.collider.gameObject.layer == p_layer)
            {
                Act();
            }
            if (poly.enabled)
            {
                poly.enabled = false;
                trig.enabled = false;
                StartCoroutine(Done());
            }
        }
    }

    private new void OnTriggerEnter2D(Collider2D col)
    {
        if (trig.IsTouchingLayers(1 << LayerMask.NameToLayer("PlayerAtk")))
        {
            if (poly.enabled)
            {
                poly.enabled = false;
                trig.enabled = false;
                StartCoroutine(Done());
            }
        }
    }

    IEnumerator Done()
    {
        var im = GetComponent<SpriteRenderer>();
        for (float i = 1; i > 0; i -= 0.05f)
        {
            im.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.03f);
        }
        Destroy(gameObject);
    }

}
