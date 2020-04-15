using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOver : MonoBehaviour
{
    public DataFiller df;
    Maintainable mt;
    // Start is called before the first frame update
    void Start()
    {
        mt = FindObjectOfType<Maintainable>();
    }

    private void Update()
    {
        if (mt != null) mt.Explode();
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("Exist"))
        {
            //데이터를 가지고, 나는 사라지고 누군가한테는 전달해야 함. 세이브포인트 씬으로 이동
            df.Fill();
            Scenemover.MoveScene(PlayerPrefs.GetString("Scene"));
        }
        else
        {
            //첫 씬으로 이동
            Destroy(df.gameObject);
            Scenemover.MoveScene("MyRoom");
        }
    }

    public void GoBack()    //"타이틀로"버튼에서만 사용됨
    {
        Scenemover.MoveScene("Title");
    }
}
