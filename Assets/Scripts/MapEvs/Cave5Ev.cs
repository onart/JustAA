using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave5Ev : MapEv
{
    public Python py;
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
}
