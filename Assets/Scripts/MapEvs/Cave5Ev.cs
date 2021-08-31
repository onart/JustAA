using System.Collections;
using UnityEngine;

public class Cave5Ev : MapEv
{
    public Python py;
    public Door ovr;
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void afterDialog()
    {
        switch (dialog)
        {
            case 50:
                StartCoroutine(e50to51());
                break;
            case 52:
                toCut(1);
                break;
        }
    }

    IEnumerator e50to51()
    {
        StartCoroutine(py.Shake());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(D_Start(51));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(D_Start(50));
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (FindObjectOfType<Glass>() == null && col.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<Python>().StopAllCoroutines();
            StartCoroutine(D_Start(52));
        }
    }
}
