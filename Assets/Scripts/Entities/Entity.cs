using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    static private GameObject space;
    private GameObject spc;   //이건 상호작용 가능 시 'space'를 띄우기 위함임
    protected Vector3 spacepos;      //'space'를 띄우는 위치

    protected Vector2 rayorigin, raydir;
    protected float raydistance;
    protected static Player p;
    protected SpriteRenderer sr;
    protected int selection;    //selection은 받은 선택지
    protected static TalkManager tm;
    public static int cooltime;            //대화 끝나면 상호작용 쿨돌리려고..
    public int dialog;         //dialog는 최근 진행한 대화
    private void Start()
    {
        if (!space) space = Resources.Load<GameObject>("Prefabs/space");
        if (tm == null) tm = FindObjectOfType<TalkManager>();
        if (p == null) p = FindObjectOfType<Player>();
        sr = GetComponent<SpriteRenderer>();
        St();
        Debug.DrawRay((Vector2)transform.position + rayorigin, raydir * raydistance, Color.green, float.PositiveInfinity);
    }
    private void Update()
    {
        Up();
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + rayorigin, raydir, raydistance, LayerMask.GetMask("Player"));
        if (cooltime > 0) { cooltime--; }
        else if (hit.collider != null)
        {
            if (spc == null) { 
                spc = Instantiate(space);
                spc.transform.position = this.transform.position + spacepos;                
            }
            if (!SysManager.forbid && Input.GetKeyDown(SysManager.keymap["상호작용"]) && p.gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero) 
            {
                ObjAct();
            }
        }
        else { Destroy(spc); }
    }

    protected IEnumerator D_Start(int i)    //선택지 뒤에 대사가 이어붙은 경우 등에 대하여 이것을 사용한다. 이외의 경우는 tm.Dialog_Start
    {
        yield return new WaitForSeconds(0.1f);
        cooltime = 0;
        tm.Dialog_Start(i, this);
    }

    public void Recieve(int i)
    {
        selection = i;
        OnRecieve();
    }

    public abstract void ObjAct();  //해당 NPE와의 상호작용
    public abstract void Up();  //추가 업데이트
    public abstract void St();  //추가 스타트
    protected abstract void OnRecieve();   //선택지를 받았을 때의 행동

    private void OnDestroy()
    {
        if (spc) Destroy(spc);
    }

    public virtual void afterDialog() { }   //대화가 종료되면 호출
    public virtual void afterSth(int flag) { }  //다용도 호출
}
