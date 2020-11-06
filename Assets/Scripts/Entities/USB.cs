using UnityEngine;
public class USB : Entity
{
    static SaveLoad sl;

    public override void ObjAct()
    {
        tm.Dialog_Start(0, this);
    }

    public override void Up()   //업데이트 함수.
    {
        if (!SysManager.menuon) transform.Rotate(Vector3.up);
    }

    public override void St()
    {
        spacepos = Vector3.up;
        if (!sl) sl = FindObjectOfType<SaveLoad>();
        rayorigin = Vector2.zero;
        raydir = Vector2.down;
        raydistance = 0.5f;
    }
    protected override void OnRecieve()
    {
        if (selection == 0) {
            sl.Save(gameObject.scene, GetComponent<Helper>().caller);
            cooltime = 0;       //직후 대사가 있는 경우 쿨타임을 0으로 줄임
            StartCoroutine(D_Start(1));
        }
    }

}
