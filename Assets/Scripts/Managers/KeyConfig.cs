using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyConfig : MonoBehaviour
{
    string skillIntro = "<특수기술 목록>\n<<,>> (대시)\n공격x3(연속 공격)\n";
    string[] skillNames = { "대시+-+공격(대시공격)", "공격 키다운 후 놓기(풀 스윙)", "특수1 키다운 후 놓기(DNEDA)" };  //BaseSet 플래그 정의와 동일한 순서로.

    //SysManager에서 정의된 키 순서 : public readonly string[] keys = { "점프", "공격", "앉기", "상호작용" };
    //이 순서대로 수동으로 끌어다 놓을 것
    public Button[] keylist;
    public TextMeshProUGUI[] labels;
    public TextMeshProUGUI skills;
    int selected;   //선택 중인 selected, -1은 미선택
    readonly KeyCode[] klist = System.Enum.GetValues(typeof(KeyCode)) as KeyCode[];

    void Start()
    {
        selected = -1;
        int sz = labels.Length;
        for (int ii = 0; ii < sz; ii++) 
        {
            labels[ii].SetText(SysManager.keys[ii] + ": " + SysManager.keymap[SysManager.keys[ii]]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selected != -1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.inputString.Length != 0)  {
                    SysManager.DictUpdate(SysManager.keys[selected], (KeyCode)(Input.inputString[0]));                    
                }
                else
                {//사실 이걸로 해도 최적화에는 큰 영향이 없긴 한데 아 몰라
                    foreach(var k in klist)
                    {
                        if (Input.GetKeyDown(k))
                        {
                            SysManager.DictUpdate(SysManager.keys[selected], k);
                            break;
                        }
                    }
                }
                labels[selected].SetText(SysManager.keys[selected] + ": " + SysManager.keymap[SysManager.keys[selected]]);
                CancelSelect();
            }
        }
    }

    public void SelectOne(int idx)
    {
        CancelSelect();
        keylist[idx].image.color = Color.yellow;
        selected = idx;
    }

    public void CancelSelect()
    {
        selected = -1;
        foreach (var b in keylist)
        {
            b.image.color = Color.white;
        }
    }

    public void SkillIntroduce()
    {
        skillIntro = "<특수기술 목록>\n<<,>> (대시)\n공격x3(연속 공격)\n";
        skills.SetText(skillIntro);
        var p = FindObjectOfType<Player>();
        int i = p.FLAGS[(int)BaseSet.Flags.SKILLS];
        int j = 0;
        while (i != 0) 
        {
            if (i % 2 == 1) skills.text += skillNames[j];
            i >>= 1;
            j++;
        }
    }
}
