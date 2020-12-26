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
        if (Input.GetKeyDown(KeyCode.R)) Load();
    }
    public void Load()
    {
        DontDestroyOnLoad(this);
        Scenemover.MoveScene("Title");
        Invoke("TitleManipulate", 0.1f);
    }

    private void TitleManipulate()
    {
        tm = FindObjectOfType<Titlemanager>();
        if (tm)
        {
            tm.Load();
            if (tm.gogo.text.Equals("새로하기"))      //저장 데이터가 없는 경우.
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
