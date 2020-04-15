using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Entity
{
    public string connectedDoor, connectedScene;        //문은 연결된 씬과 문을 가리킨다. string은 상대 게임오브젝트 이름과 비교하기 위함
    public Sprite sp;
    public bool mode;                                   //이벤트의 편의를 위해 대화 모드와 이동 모드를 따로 다루자. 이를 테면, 문이 잠긴 경우 대화모드여야 한다. true: 이동 모드, false : 대화 모드

    public int response;                                
    //대화 모드일 때 불러낼 대사로, 에딧 뷰에서 정하면 된다. 아마 리시브가 있을 수도 있는데 response에 따른 분기로 하면 하나로 된다.
    //열쇠가 있는 경우와 없는 경우는 플레이어 플래그 또는 아이템 정보를 통해 알 수 있을 것 같다.

    Fade2 fio; //맵 이동 시 페이드 아웃->페이드 인

    public override void Up()
    {
        
    }

    public override void ObjAct()
    {
        if (mode)
        {
            sr.sprite = sp;
            p.doorname = connectedDoor;
            fio.gameObject.SetActive(true);
            fio.DOOR = this;
        }
        else
        {
            tm.Dialog_Start(response, this);
        }
    }
    public override void St()
    {
        spacepos = new Vector3(-0.2f, 1.3f, 0);
        fio = FindObjectOfType<Fade2>();
        if (p.doorname == name) p.transform.position = transform.position;
        rayorigin = Vector2.zero;
        raydir = Vector2.up + Vector2.left * 0.5f;
        raydistance = 1;
    }
    
    protected override void OnRecieve() //공함수
    {        
    }
    public void DoorOpen()
    {
        Scenemover.MoveScene(connectedScene);
    }


}
