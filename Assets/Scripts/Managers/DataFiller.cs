using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFiller : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Fill()
    {
        Invoke("Datafill", 0.02f);        
    }

    void Datafill()
    {
        var p = FindObjectOfType<Player>();
        if (p == null)  //혹시 렉걸려서 못찾으면 다 망가지므로 취한 조치
        {
            Invoke("Datafill", 0.02f);
        }
        else {
            p.HP = PlayerPrefs.GetInt("CurrentHp");
            p.MHP = PlayerPrefs.GetInt("MHP");
            for (int i = 0; i < (int)BaseSet.Flags.FLAGCOUNT; i++)
            {
                if (!PlayerPrefs.HasKey("Fl" + i)) { PlayerPrefs.SetInt("Fl" + i, 0); }    //업데이트를 하는 경우, 이는 오류가 아님
                p.FLAGS[i] = PlayerPrefs.GetInt("Fl" + i);   //이벤트 플래그 불러오기
            }
            Destroy(gameObject);
        }
    }
}
