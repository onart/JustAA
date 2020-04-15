using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade2 : MonoBehaviour      //문을 열면 페이드 아웃->이동->페이드 인을 수행. 이것은 오직 문만을 위한 클래스임
{
    Image im;
    float br;                         //밝기 지수
    bool gamestart;

    Door dr;

    public Door DOOR {
        set { dr = value; }
    }
    void Start()
    {
        gamestart = true;
        br = 0;
        dr = null;
        im = GetComponent<Image>();
    }

    void Update()
    {
        if (gamestart)
        {
            gameObject.SetActive(false);
            gamestart = false;
        }
        br += 0.02f;
        if (br < 1) im.color = new Color(0, 0, 0, br);
        else if (br <= 1.02) { im.color = new Color(0, 0, 0, 1); dr.DoorOpen(); }
        else if (br < 2) im.color = new Color(0, 0, 0, 2 - br);
        else
        {
            br = 0;
            gameObject.SetActive(false);
        }
     }
}
