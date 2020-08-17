﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkManager : MonoBehaviour
{
    private int state, idx, wait;  //대화 진행 상태. 디폴트 -1 상태에서는 대화상자 OFF 상태. wait는 오류수정용 1프레임 대기. wait=2인 경우 선택지가 켜져 있음
    public int selection, opcount;
    public TextMeshProUGUI notice, arrow;

    Entity ent; //선택지 답을 받을 개체. 이벤트는 개체별로 존재
    TextMeshProUGUI options;
    Image optBox;
    DialogBox dBox;
    public GameObject dBoxUI, optionUI;

    private void Start()
    {
        Note();
        state = -1;
        wait = 0;
        dBox = dBoxUI.GetComponent<DialogBox>();
        options = optionUI.GetComponentInChildren<TextMeshProUGUI>();
        optBox = optionUI.GetComponentInChildren<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) print(Entity.cooltime);
        if (!SysManager.menuon)
        {
            if (wait == 0 && Input.GetKeyDown(SysManager.keymap["상호작용"]) && state > -1) //대화 넘기기 페이즈
            {
                Dialog_Go();
            }
            else if (wait == 1) wait = 0;
            else if (wait == 2)                                         //선택지 페이즈
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (--selection < 0) selection = opcount - 1;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (++selection > opcount - 1) selection = 0;
                }
                RenderArrow();
                if (Input.GetKeyDown(SysManager.keymap["상호작용"])) 
                {
                    SelectOff();
                }
            }
        }
    }

    void RenderArrow()
    {
        arrow.text = "";
        for (int i = 0; i < opcount; i++) 
        {
            if (i == selection) arrow.text += " >";
            else arrow.text += "\n";
        }
        arrow.text += ' ';
    }

    void SelectON(Options op)
    {
        if (op.Number == 0)
        {
            BoxOFF();
            return;
        }
        Vector2 v2;
        wait = 2;
        opcount = op.Number;
        optionUI.SetActive(true);
        optBox.rectTransform.localScale = new Vector2(op.Hor, op.Number);
        options.text = op.Body;
        if (op.Number != 0) { v2 = new Vector2(1 / op.Hor, (float)1 / op.Number); }
        else { v2 = Vector2.zero; }
        options.rectTransform.localScale = v2;
        arrow.rectTransform.localScale = v2;
        selection = 0;
    }

    void SelectOff()
    {
        optionUI.SetActive(false);
        if (ent != null) ent.Recieve(selection);
        BoxOFF();
    }

    void BoxON()
    {
        dBoxUI.SetActive(true);
    }
    void BoxOFF()
    {
        wait = 1;
        state = -1;
        Entity.cooltime = 10;
        dBoxUI.SetActive(false);
        ent = null;
    }
    public void Dialog_Start(int index, Entity en)
    {
        if (state == -1 && Entity.cooltime == 0)
        {
            BoxON();
            ent = en;
            en.dialog = index;
            idx = index;
            wait = 1;
            Dialog_Go();
        }
    }
    public void Dialog_Go() //인자 : 위의 talkData의 리스트.
    {
        if (dBox.ended) {
            state++;
            var temp1 = BaseSet.talkData[idx];
            var temp2 = temp1.Item1[state];
            if (temp2.Item1 == BaseSet.Chars.END)
            {
                SelectON(BaseSet.ops[temp1.Item2]);
                return;
            }
            dBox.InitBox(temp2.Item1, temp2.Item2, temp2.Item3);
        }
        else
        {
            var temp1 = BaseSet.talkData[idx];
            var temp2 = temp1.Item1[state];
            dBox.InstantInitBox(temp2.Item3);
        }
    }

    void Note()
    {
        notice.text = "";        
    }

    public void NoteFor(string s, float time = 2.0f)
    {
        //텍스트 1 발동 후 텍스트 2 바로 발동 시 텍스트 2가 지워지는 시간이 텍스트 1 기준인 문제 수정
        CancelInvoke("Note");
        notice.text = s;
        Invoke("Note", time);
    }
}
