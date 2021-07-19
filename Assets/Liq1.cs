using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liq1 : Attacker
{
    public Sprite[] shape;
    

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = shape[Random.Range(0, shape.Length)];
    }

    private new void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("PlayerAtk") || col.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            StartCoroutine(diminish());
        }
        else
        {
            base.OnTriggerEnter2D(col);
        }
    }

    IEnumerator diminish()
    {
        enabled = false;
        for(int i = 0; i < 8; i++)
        {
            transform.localScale = Vector2.one * (8 - i) / 8;
            yield return new WaitForSeconds(0.03f);
        }
        Destroy(gameObject);
    }

    protected override void Act()
    {
        base.Act();
        StartCoroutine(diminish());
    }
}
