using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade2 : MonoBehaviour      //문을 열면 페이드 아웃->이동->페이드 인을 수행. 이것은 오직 문만을 위한 클래스임
{
    Image im;
    float br;                         //밝기 지수
    bool gamestart;

    Door dr;

    public Door DOOR
    {
        set { dr = value; }
    }
    void Start()
    {
        gamestart = true;
        br = 0;
        dr = null;
        im = GetComponent<Image>();
    }

    IEnumerator fio()
    {
        for (br = 0; br < 1; br += 0.05f)
        {
            im.color = new Color(0, 0, 0, br);
            yield return new WaitForSecondsRealtime(0.025f);
        }
        im.color = Color.black;
        dr.DoorOpen();
        FindObjectOfType<Player>().onground = false;
        for (; br > 0; br -= 0.05f)
        {
            im.color = new Color(0, 0, 0, br);
            yield return new WaitForSecondsRealtime(0.025f);
        }
        im.color = Color.clear;
        gameObject.SetActive(false);
    }

    public void f2()
    {
        StartCoroutine(nameof(fio));
    }



    void Update()
    {
        if (gamestart)
        {
            gameObject.SetActive(false);
            gamestart = false;
        }
    }

}
