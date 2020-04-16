﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//매니저의 우두머리격 존재로, 겹치는 부분이 있으면 여기서 허락을 받도록 할 것
public class SysManager : MonoBehaviour
{
    Canvas cv;
    public GameObject menu, dialogUI;
    public static bool menuon = false;          //메뉴가 열려있는가
    public static bool forbid = false;          //통상 조작을 봉인할까
    public static Dictionary<string, KeyCode> keymap = new Dictionary<string, KeyCode> { };
    public readonly string[] keys = { "Jmp", "Atk", "Sit", "Obj" };

    private void Start()
    {
        Application.targetFrameRate = 60;   //어차피 기본 60이다. 테스트 플레이에서 180fps길래 추가함+프레임드랍 상정        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuFlip();
        }
        SettimeScale();
    }

    void MenuFlip()
    {
        menu.SetActive(!menu.activeSelf);
        menuon = menu.activeSelf;
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

    public void KeyMapUp(string s, KeyCode key)  //키맵 업데이트. 설정을 통해 바꾸거나, 처음 시작할 때 정해지거나.        
    {
        
    }

    public void KeyMapSave()                    //키맵 저장.
    {
        foreach(var c in keymap)
        {
            PlayerPrefs.SetInt(c.Key, (int)c.Value);
        }
        PlayerPrefs.Save();
    }

    public void KeyMapLoad()
    {
        foreach(var c in keys)
        {
            keymap.Add(c, (KeyCode)PlayerPrefs.GetInt(c));
        }
    }

}
