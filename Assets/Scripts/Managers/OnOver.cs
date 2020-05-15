using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOver : MonoBehaviour
{
    Titlemanager tm;
    Maintainable mt;
    // Start is called before the first frame update
    void Start()
    {
        mt = FindObjectOfType<Maintainable>();
    }

    private void Update()
    {
        if (mt) mt.Explode();
    }
    public void Load()
    {
        DontDestroyOnLoad(this);
        Scenemover.MoveScene("Title");
        Invoke("TitleManipulate", 0.03f);
    }

    private void TitleManipulate()
    {
        tm = FindObjectOfType<Titlemanager>();
        if (tm)
        {
            tm.Load();
            if (tm)      //저장 데이터가 없는 경우. 그 외에 그새 데이터가 손상된 경우에도 이렇게 오는데 그건 버그 아니다. 애초에 조작 시도한 놈이 잘못이다.
            {
                tm.DfSelect(SysManager.difficulty);
            }
            Destroy(gameObject);
        }
        else Invoke("TitleManipulate", 0.02f);
    }

    public void GoBack()    //"타이틀로"버튼에서만 사용됨
    {
        Scenemover.MoveScene("Title");
    }
}
