using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyConfig : MonoBehaviour
{
    const string skillIntro = "<<,>> (대시)\n공격x3\t(연속 공격)\n";
    readonly string[] skillNames = { "대시 - 공격\t(대시공격)\n", "공격 키다운\t(박치기)\n", "특수1 키다운 -> 좌/우 방향키와 함께 놓기\t(공중 대시)\n", "공중에서 좌상우하/우상좌하+공격\t(붕붕이)" };  //BaseSet 플래그 정의와 동일한 순서로.

    //SysManager에서 정의된 키 순서 : public readonly string[] keys = { "점프", "공격", "앉기", "상호작용", "특수1", "상향" };
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
                if (Input.inputString.Length != 0)
                {
                    SysManager.DictUpdate(SysManager.keys[selected], (KeyCode)(Input.inputString[0]));
                }
                else
                {//사실 이걸로 해도 최적화에는 큰 영향이 없긴 한데 아 몰라
                    foreach (var k in klist)
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
        skills.text = skillIntro;
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
