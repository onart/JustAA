using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFiller : MonoBehaviour
{
    public int nando;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void Fill()
    {
        Invoke("Datafill", 0.02f);        
    }

    public void NewFill()
    {
        Invoke("Sys", 0.02f);
    }

    void Datafill()
    {
        var p = FindObjectOfType<Player>();
        if (p == null)  //혹시 렉걸려서 못찾으면 다 망가지므로 취한 조치
        {
            Invoke("Datafill", 0.02f);
        }
        else {
            SysManager.difficulty = nando;
            p.HP = PlayerPrefs.GetInt("CurrentHp");
            p.MHP = PlayerPrefs.GetInt("MHP");
            p.exp = PlayerPrefs.GetInt("EXP", 0);
            for (int i = 0; i < (int)BaseSet.Flags.FLAGCOUNT; i++)
            {
                p.FLAGS[i] = PlayerPrefs.GetInt("Fl" + i, 0);      //이벤트 플래그 불러오기                            
            }            
            Destroy(gameObject);
        }
    }

    private void Sys()
    {
        var s = FindObjectOfType<SysManager>();
        if (s == null)
        {
            Invoke("Sys", 0.02f);
        }
        else
        {
            SysManager.difficulty = nando;
            Destroy(gameObject);
        }
    }
}
