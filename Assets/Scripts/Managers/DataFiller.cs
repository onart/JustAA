using System.Collections;
using UnityEngine;

public class DataFiller : MonoBehaviour //데이터의 덩어리로, 씬에 잠깐 이동하여 데이터를 불어넣음
{
    //로드변수. 배포가 이루어진 상태에서 새로운 세이브/로드변수가 추가되는 경우, 반드시 디폴트값을 추가하여 새로 생긴 요소에 대하여는 디폴트값으로 초기화하자.
    public int diff, mhp, hp, caller = -1, exp;
    public string map;
    int[] pFlag = new int[(int)BaseSet.Flags.FLAGCOUNT];
    public static bool load_complete;
    public int[] PF
    {
        get { return pFlag; }
    }
    //----------------


    void Start()
    {
        DontDestroyOnLoad(this);
        load_complete = false;
    }

    public void Fill()
    {
        StartCoroutine(Datafill());
    }

    public void NewFill()
    {
        StartCoroutine(Sys());
    }

    IEnumerator Datafill()
    {
        Player p = null;
        while (!p)
        {
            p = FindObjectOfType<Player>();
            yield return new WaitForEndOfFrame();
        }
        SysManager.difficulty = diff;
        SysManager.cbr = 24 - diff;
        p.HP = hp;
        p.MHP = mhp;
        p.mhpCheck = 24 - mhp;
        p.exp = exp;
        load_complete = true;
        Destroy(gameObject);
        for (int i = 0; i < (int)BaseSet.Flags.FLAGCOUNT; i++)
        {
            p.FLAGS[i] = pFlag[i];      //이벤트 플래그 불러오기
        }
    }

    IEnumerator Sys()
    {
        while (!FindObjectOfType<SysManager>())
        {
            yield return new WaitForEndOfFrame();
        }
        SysManager.difficulty = diff;
        SysManager.cbr = 24 - diff;
        load_complete = true;
        Destroy(gameObject);
    }

}
