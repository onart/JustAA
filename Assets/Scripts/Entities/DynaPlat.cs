using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynaPlat : MonoBehaviour
{
    Rigidbody2D rb2d;
    int min;
    public Vector2 v0;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = v0;
        min = 1;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            min = -min;
            rb2d.velocity = min * v0;
        }
    }
}
