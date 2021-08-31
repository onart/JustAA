using UnityEngine;

public class PyHead : MonoBehaviour
{
    int lay;
    Python bdy;
    float tp;

    // Start is called before the first frame update
    void Start()
    {
        lay = LayerMask.NameToLayer("Map");
        bdy = GetComponentInParent<Python>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (Time.time > tp && col.collider.gameObject.layer == lay)
        {
            tp = Time.time + 1;
            bdy.at.enabled = false;
            StartCoroutine(bdy.still(0.5f));
        }
    }
}
