using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    const float speed = 4;
    const float downlim = -10;

    public string doorname;
    public bool onground, attking;    //onground : 땅에 있는지. kdown : 강공격에 맞은 경우, attking : 공격중인가?
    public int kdown;
    public float jumphold;
    int[] flags = new int[(int)BaseSet.Flags.FLAGCOUNT];     //BaseSet.cs에 명시된 플래그 기반. 데이터필러에서 이것도 채워야 한다. 꼭 0과 1로 구분할 필요는 없음

    public Rigidbody2D rb2d;

    bool frz;       //맞은 뒤 경직은 이걸로(아마 이걸 푸는 스킬도 만들수도)
    int hp;
    int MAXHP = 100;
    public int expe;
    Animator anim;
    SpriteRenderer sr;

    private GameObject dmgTxt, dmgTxtInst;  //대미지 텍스트의 prefab이랑 인스턴스
    private DmgOrHeal doH;

    public int[] FLAGS
    {
        get { return flags; }
    }

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public int MHP
    {
        get { return MAXHP; }
        set { MAXHP = value; }
    }

    public int exp
    {
        get { return expe; }
        set { expe = value; }
    }

    void Start()
    {
        expe = 0;
        dmgTxt = Resources.Load<GameObject>("Prefabs/dmgTxt");
        attking = false;
        doorname = "";
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        jumphold = 0;
        hp = MAXHP;
        kdown = 0;
        HpChange(0);
    }

    void Update()
    {
        if (!SysManager.forbid && hp > 0) 
        {
            if (frz)
            {
                //일단 조작만 봉인
            }
            else {
                Sit();
                LRmove();
                Air();
            }
        }
    }

    void LRmove()
    {
        float lr = Input.GetAxis("Horizontal");
        anim.SetFloat("LR", Fabs(lr));
        if (!attking && !anim.GetBool("SIT")) 
        {
            rb2d.velocity = new Vector2(lr * speed, rb2d.velocity.y);
            //공격중 방향전환 가능?불가능? 아직 미정
            if (lr < 0) transform.localScale = new Vector2(-0.3f, 0.3f);
            else if (lr > 0) transform.localScale = new Vector2(0.3f, 0.3f);
            //--------------------------------------
        }
        else if (onground) rb2d.velocity = new Vector2(0, rb2d.velocity.y);  //
    }

    void Air()
    {
        anim.SetBool("AIR", !onground);
        //점프부--------------------------------------
        if (onground && Input.GetKeyDown(SysManager.keymap["점프"]))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y + 4.5f);
            jumphold = 62;  //최대 높이에 도달 못한 상태로 키다운하면서 떨어진 후 점프할 때 최대 높이에 도달 못하는 문제 수정
        }
        else if (jumphold > 0 && Input.GetKey(SysManager.keymap["점프"])) 
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y + Time.deltaTime * jumphold * 0.12f);
            jumphold -= (60 * Time.deltaTime);  //여긴 이렇게 하는 게 맞음. 매 순간마다 크게 바뀌는 경우를 상정하면 나머지와 달리 영향이 큼
        }
        if (Input.GetKeyUp(SysManager.keymap["점프"]) && !onground) 
        {
            jumphold = 0;
        }
        //--------------------------------------------
        if (rb2d.velocity.y < downlim) 
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, downlim);
        }
    }

    void Sit()
    {
        if (Input.GetKey(SysManager.keymap["앉기"]) && onground && Time.timeScale > 0) 
        {
            anim.SetBool("SIT", true);
        }
        else
        {
            anim.SetBool("SIT", false);
        }
    }
    float Fabs(float f)
    {
        return f > 0 ? f : -f;
    }

    void StandUp()
    {
        kdown = 0;
        frz = false;
        anim.SetInteger("OVR", 0);
    }

    void Hp0()
    {
        Scenemover.MoveScene("GameOver");
    }

    public void HpChange(int delta)     //체력 회복 or 피해, ui부는 나중에 옮기는 게 나을 수도 있음
    {
        if (delta == 0) return;
        hp += delta;
        dmgTxtInst = Instantiate(dmgTxt);
        dmgTxtInst.transform.position = transform.position;
        doH = dmgTxtInst.GetComponent<DmgOrHeal>();
        if (delta < 0)
        {
            doH.SetText((-delta).ToString(), Color.red);
        }
        else
        {
            doH.SetText(delta.ToString(), Color.green);
        }

        if (hp > MAXHP) hp = MAXHP;
        else if (hp <= 0)
        {
            hp = 0;
            anim.SetTrigger("HIT");
            anim.SetInteger("OVR", 2);
            Invoke("Hp0", 1);
        }
    }

    public void GetHit(int dmg, int down) 
    {
        //적의 공격은 적 충돌에서 이 함수를 불러서 사용, 
        //down 0은 피격 모션 없음, down 1은 피격모션만 있음, down 2는 누움.누운 상태에서는 콜라이더 숨김
        HpChange(-dmg);
        if (down != 0) {
            anim.SetTrigger("HIT");
            anim.SetInteger("OVR", 0);
            kdown = down;
            frz = true;
            Invoke("Hold", 0.3f);
        }
    }

    public void Hold()                 //맞은 후 경직.
    {
        if (!onground) return;
        if (kdown == 2)  {            
            anim.SetInteger("OVR", 2);
            if (hp > 0) Invoke("StandUp", 0.5f);
        }
        else
        {
            anim.SetInteger("OVR", 1);
            frz = false;
        }
    }

    public void GainExp(int delta)
    {
        expe += delta;
        if (delta > 0)
        {
            dmgTxtInst = Instantiate(dmgTxt);
            dmgTxtInst.transform.position = transform.position;
            doH = dmgTxtInst.GetComponent<DmgOrHeal>();
            doH.SetText("+" + delta.ToString(), Color.yellow, 2);
        }
    }

}