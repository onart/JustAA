using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyConfig : MonoBehaviour
{
    //SysManager에서 정의된 키 순서 : public readonly string[] keys = { "점프", "공격", "앉기", "상호작용" };
    //이 순서대로 수동으로 끌어다 놓을 것
    public Button[] keylist;
    public TextMeshProUGUI[] labels;
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

    void CancelSelect()
    {
        selected = -1;
        foreach (var b in keylist)
        {
            b.image.color = Color.white;
        }
    }
}
