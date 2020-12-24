using UnityEngine;

public class Six6Ev : MapEv
{
    public Animator anim;
    public J1 j1;
    public BossHp bigBar;
    public Camera cv;
    public Cinemachine.CinemachineVirtualCamera cc;

    int sh;

    protected override void Stt()
    {
        j1.enabled = false;
        bigBar.gameObject.SetActive(false);
        sh = 0;
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
                getFollow();
                break;
            case 37:
                //똑 부러지는 컷신(이미지만 달리 하는 전용 씬)
                cooltime = 0;
                p.FLAGS[(int)BaseSet.Flags.STAGE1] = 6;
                toCut(0);
                break;
        }
    }

    public override void afterSth(int flag)
    {
        tm.Dialog_Start(36, this);
        p.FLAGS[(int)BaseSet.Flags.STAGE1] = 6;
    }

    void getFollow()
    {
        Destroy(cc.gameObject);
        shake();
    }

    void shake()
    {
        if (sh < 10)
        {
            sh++;
            cv.transform.position += Mathf.Pow(-1, sh) * Vector3.left / 2;
            Invoke(nameof(shake), 0.03f);
        }
        else
        {
            cooltime = 0;
            tm.Dialog_Start(37, this);
            return;
        }
    }
}
