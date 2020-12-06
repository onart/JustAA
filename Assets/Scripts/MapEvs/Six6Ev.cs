using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Six6Ev : MapEv
{
    public Animator anim;
    public J1 j1;
    public BossHp bigBar;

    private void Start()
    {
        j1.enabled = false;
        bigBar.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            tm.Dialog_Start(35, this);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public override void afterDialog()
    {
        switch (dialog)
        {
            case 35:
                anim.SetTrigger("IO");
                bigBar.gameObject.SetActive(true);
                j1.enabled = true;
                break;
            case 36:
                //카메라 흔들
                cooltime = 0;
                tm.Dialog_Start(37, this);
                break;
            case 37:
                //똑 부러지는 이벤트
                break;
        }
    }

    public override void afterSth(int flag)
    {
        tm.Dialog_Start(36, this);
        p.FLAGS[(int)BaseSet.Flags.STAGE1] = 6;
    }
}
