using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private GameObject space, spc;   //이건 상호작용 가능 시 'space'를 띄우기 위함임
    protected Vector3 spacepos;      //'space'를 띄우는 위치

    protected Vector2 rayorigin, raydir;
    protected float raydistance;
    protected static Player p;
    protected SpriteRenderer sr;
    protected int selection;
    protected static TalkManager tm;
    public int cooltime;            //대화 끝나면 상호작용 쿨돌리려고..

    private void Start()
    {
        space = Resources.Load<GameObject>("Prefabs/space");
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
        if (hit.collider != null && p.gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            if (spc == null) { spc = Instantiate(space, this.transform.position + spacepos, Quaternion.identity); }
            if (!SysManager.forbid && Input.GetKeyDown(SysManager.keymap["상호작용"])) 
            {
                if (cooltime > 0) cooltime = 0;
                else ObjAct();
            }
        }
        else { Destroy(spc); }
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
}
