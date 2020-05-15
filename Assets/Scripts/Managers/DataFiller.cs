using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataFiller : MonoBehaviour //데이터의 덩어리로, 씬에 잠깐 이동하여 데이터를 불어넣음
{
    //로드변수. 배포가 이루어진 상태에서 새로운 세이브/로드변수가 추가되는 경우, 반드시 디폴트값을 추가하여 새로 생긴 요소에 대하여는 디폴트값으로 초기화하자.
    public int diff, mhp, hp, caller = -1, exp;
    public string map;
    int[] pFlag = new int[(int)BaseSet.Flags.FLAGCOUNT];
    public int[] PF
    {
        get { return pFlag; }
    }
    //----------------


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
            SysManager.difficulty = diff;
            SysManager.cbr = 24 - diff;
            p.HP = hp;
            p.MHP = mhp;
            p.mhpCheck = 24 - mhp;
            p.exp = exp;            
            for (int i = 0; i < (int)BaseSet.Flags.FLAGCOUNT; i++)
            {
                p.FLAGS[i] = pFlag[i];      //이벤트 플래그 불러오기
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
            SysManager.difficulty = diff;
            SysManager.cbr = 24 - diff;
            Destroy(gameObject);
        }
    }    

}
