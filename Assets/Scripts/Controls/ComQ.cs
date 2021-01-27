using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComQ : MonoBehaviour
{
    enum Candid { ATK, JUMP, LEFT, RIGHT, UP, DOWN, NONE };
    public Image[] visualQ = new Image[6];
    public Image gauge;
    public Sprite[] entry = new Sprite[7];
    Queue<Candid> q = new Queue<Candid>();

    Player p;
    Animator anim;
    Rigidbody2D rb2d;
    int inputime;
    const int inpu = 12;

    //커맨드 리스트
    static readonly Candid[] L_RUSH = { Candid.LEFT, Candid.LEFT };
    static readonly Candid[] R_RUSH = { Candid.RIGHT, Candid.RIGHT };
    static readonly Candid[] COMBO1 = { Candid.ATK };
    static readonly Candid[] COMBO2 = { Candid.ATK, Candid.ATK };
    static readonly Candid[] COMBO3 = { Candid.ATK, Candid.ATK, Candid.ATK };
    static readonly Candid[] L_DAT = { Candid.LEFT, Candid.LEFT, Candid.NONE, Candid.ATK };
    static readonly Candid[] R_DAT = { Candid.RIGHT, Candid.RIGHT, Candid.NONE, Candid.ATK };
    static readonly Candid[] AIRSP_L = { Candid.RIGHT, Candid.UP, Candid.LEFT, Candid.ATK };
    static readonly Candid[] AIRSP_R = { Candid.LEFT, Candid.UP, Candid.RIGHT, Candid.ATK };
    //커맨드 리스트
    void Start()
    {
        p = GetComponent<Player>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        inputime = inpu;
        q.Enqueue(Candid.NONE);
        q.Enqueue(Candid.NONE);
        q.Enqueue(Candid.NONE);
        q.Enqueue(Candid.NONE);
        q.Enqueue(Candid.NONE);
        q.Enqueue(Candid.NONE);
    }

    void Update()
    {
        if (!SysManager.forbid)
        {
            if (p.kdown == 2) { QUp(Candid.NONE); return; }
            inputime--;
            QIn();
            if (inputime == 0)
            {
                QUp(Candid.NONE);
                anim.SetInteger("COMBO", 0);
            }
            if (inputime > 200) inputime = 1;   //이건 deltaTime값이 0이 되는 특수한 경우(대화 등)에 대한 대처
            gauge.transform.localScale = new Vector2((float)inputime / inpu * (60 * Time.deltaTime), 1);
        }
    }

    void UpdateVisual()
    {
        var t = q.ToArray();
        for(int i = 0; i < 6; i++)
        {
            visualQ[i].sprite = entry[(int)t[i]];
        }
    }
    void QUp(Candid c)
    {
        q.Dequeue();
        q.Enqueue(c);
        UpdateVisual();
        if (anim.GetInteger("COMBO") > 0) inputime = (int)(inpu / (40 * Time.deltaTime));
        else inputime = (int)(inpu / (60 * Time.deltaTime));
        QAct();
    }

    void QIn()
    {
        if (Input.GetKeyDown(SysManager.keymap["공격"])) QUp(Candid.ATK);
        if (Input.GetKeyDown(SysManager.keymap["점프"])) QUp(Candid.JUMP);
        if (Input.GetKeyDown(SysManager.keymap["앉기"])) QUp(Candid.DOWN);
        if (Input.GetKeyDown(SysManager.keymap["상향"])) QUp(Candid.UP);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) QUp(Candid.LEFT);
        if (Input.GetKeyDown(KeyCode.RightArrow)) QUp(Candid.RIGHT);
    }


    bool QRead(Candid[] command, int size)
    {
        if (size > 6) return false;
        int i = 0;
        foreach(Candid c in command)
        {
            if (q.ToArray()[6 - size + i] != c) 
            {
                return false;
            }
            i++;
        }

        return true;
    }
    
    void QAct()
    {
        if (SkillKnow(3) && !p.onground && (QRead(AIRSP_R, 4) || QRead(AIRSP_L, 4))) 
        { anim.SetInteger("COMBO", 11); }   //회전 중 위치를 고정할지 아닐지는 미정
        else if(SkillKnow(0) && p.onground && !anim.GetBool("SIT") && (QRead(L_DAT,4) || QRead(R_DAT, 4)))
        { anim.SetInteger("COMBO", 11); }
        else if (p.onground && !anim.GetBool("SIT") && (QRead(L_RUSH, 2) || QRead(R_RUSH, 2)))
        { anim.SetTrigger("RUSH"); rb2d.MovePosition(rb2d.position + Vector2.right * Input.GetAxisRaw("Horizontal")); q.Enqueue(Candid.NONE); q.Dequeue(); }
        else if (QRead(COMBO3, 3))
        { anim.SetInteger("COMBO", 3); /*q.Enqueue(Candid.NONE); q.Dequeue();*/ }
        else if (QRead(COMBO2, 2))
        { anim.SetInteger("COMBO", 2); }
        else if (QRead(COMBO1, 1))
        { anim.SetInteger("COMBO", 1); }
        else anim.SetInteger("COMBO", 0);
    }

    bool SkillKnow(int skill)   //고급 스킬을 배웠는지
    {
        return (p.FLAGS[(int)BaseSet.Flags.SKILLS] >> skill) % 2 == 1;
    }
}
