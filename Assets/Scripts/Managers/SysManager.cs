﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//매니저의 우두머리격 존재로, 겹치는 부분이 있으면 여기서 허락을 받도록 할 것
public class SysManager : MonoBehaviour
{
    Canvas cv;
    public Toggle fpsCheck;

    public GameObject menu, dialogUI, fade;
    public KeyConfig kc;                        //키설정용
    public static bool menuon = false;          //메뉴가 열려있는가
    public static bool forbid = false;          //통상 조작을 봉인할까
    public static Dictionary<string, KeyCode> keymap = new Dictionary<string, KeyCode> { };
    public static readonly string[] keys = { "점프", "공격", "앉기", "상호작용", "특수1", "상향" }; //좌우 키, 메뉴 키 변경은 금지.
    public static int difficulty = 0;               //1: 쉬움, 2: 보통, 3: 어려움
    public static int cbr = 24;    //치트 블로커

    private void Start()
    {
        getFrameRate();
        KeyMapLoad();
    }

    void getFrameRate()
    {
        if (PlayerPrefs.GetInt("FPSis60", 1) == 1)
        {
            Application.targetFrameRate = 60;
            fpsCheck.isOn = true;
        }
        else
        {
            Application.targetFrameRate = 30;
            fpsCheck.isOn = false;
        }
    }

    public void setFrameRate()
    {
        if (fpsCheck.isOn) PlayerPrefs.SetInt("FPSis60", 1);
        else PlayerPrefs.SetInt("FPSis60", 0);
        getFrameRate();
    }

    void Update()
    {
        Checker();  //치트체커
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!fade.activeSelf) MenuFlip();
        }
        SettimeScale();
    }

    void MenuFlip()
    {
        menu.SetActive(!menu.activeSelf);
        menuon = menu.activeSelf;
        if (menuon)
        {
            kc.CancelSelect();
            kc.SkillIntroduce();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    void SettimeScale()
    {
        if (!menuon && !dialogUI.activeSelf) { Time.timeScale = 1; forbid = false; }
        else { Time.timeScale = 0; forbid = true; }
    }

    public void GoBack()    //"타이틀로"버튼에서만 사용됨
    {
        Scenemover.MoveScene("Title");
    }

    public static void KeyMapSave()                    //키맵 저장.
    {
        foreach (var c in keymap)
        {
            PlayerPrefs.SetInt(c.Key, (int)c.Value);
        }
        PlayerPrefs.Save();
    }

    public void KeyMapLoad()                    //키맵 로드.
    {
        DictUpdate("왼쪽", KeyCode.LeftArrow);        //여기는 유저에게 보여줘서는 안 되며, 내가 수정해서도 안 되는 매핑
        DictUpdate("오른쪽", KeyCode.RightArrow);
        DictUpdate("메뉴", KeyCode.Escape);
        foreach (var c in keys)
        {
            if (PlayerPrefs.HasKey(c)) DictUpdate(c, (KeyCode)PlayerPrefs.GetInt(c));
            //업데이트로 키 이름이 바뀌지 않아야 함. 하지만 혹시 모르니
        }
        SetDefaultKeyMap();                     //업데이트로 새로운 키가 추가될 수 있으므로
    }

    void SetDefaultKeyMap()
    {
        DictUpdate2("점프", KeyCode.Z);
        DictUpdate2("공격", KeyCode.X);
        DictUpdate2("앉기", KeyCode.DownArrow);
        DictUpdate2("상호작용", KeyCode.Space);
        DictUpdate2("특수1", KeyCode.LeftShift);
        DictUpdate2("상향", KeyCode.UpArrow);
    }

    public static void DictUpdate(string key, KeyCode value)
    {
        foreach (var v in keymap.Values)    //키 겹침 허용 x
        {
            if (value == v) return;
        }
        if (keymap.ContainsKey(key))
        {
            keymap.Remove(key);
        }
        keymap.Add(key, value);
    }

    void DictUpdate2(string key, KeyCode value) //순서에 따라 디폴트키로 되돌아오는 경우가 생겨서 디폴트키 방식 변경(키 있음->취소, 키 없는데 값 겹침->NONE추가) 
    {
        if (keymap.ContainsKey(key))
        {
            return;
        }
        foreach (var v in keymap.Values)    //(누르는)키 겹침 허용 x
        {
            if (value == v)
            {
                keymap.Add(key, KeyCode.None);
                return;
            }
        }
        keymap.Add(key, value);
    }

    void Checker()
    {
        if (cbr != 24 - difficulty)
        {
            print("조작 발생!!");
            difficulty = 24 - cbr;
            //Scenemover.MoveScene("GameOver"); //이유 없이 떠서 테스트에 방해되기도 하니 보류
        }
    }

}
