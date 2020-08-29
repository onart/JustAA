using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Six6Ev : MapEv
{
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            tm.Dialog_Start(35, this);
            anim.SetTrigger("IO");
            Destroy(gameObject);
        }
    }
}
