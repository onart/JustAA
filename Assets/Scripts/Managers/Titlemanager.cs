using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Titlemanager : MonoBehaviour
{
    readonly string[] difficulty = { "쉬움", "보통", "어려움" };

    public TextMeshProUGUI ertext, gogo, counts, dataDisp;
    public DataFiller df;
    public GameObject nanedo;

    double nanido = -1;
    int mgn;
    string flags;

    void DisplayData2()
    {
        if (df.diff == 0) { 
            dataDisp.SetText("데이터가 손상되었습니다. 데이터를 삭제해 주세요.");
            return;
        }
        string hps = "HP " + df.hp + "/" + df.mhp;
        dataDisp.SetText(BaseSet.Maps[df.map] + ' ' + '\n' + hps + ", " + df.exp + "xp\n" + difficulty[df.diff - 1]);
    }

    int Validity()  //난이도 숫자의 저장 데이터의 유효성 판단. 역연산 후 절대오차가 작으면 통과한다.
    {
        mgn -= (df.map[0] - 'A');
        mgn -= df.mhp;
        int down = 2;
        foreach (int flag in df.PF) {
            down += flag;
        }
        float candid = Mathf.Log((float)nanido, down);
        candid--;
        candid *= mgn;
        //Debug.Log(candid);
        if (Mathf.Abs(candid - 1) < 0.0001f) return 1;             //난이도 쉬움
        else if (Mathf.Abs(candid - 2) < 0.0001f) return 2;        //난이도 보통
        else if (Mathf.Abs(candid - 3) < 0.0001f) return 3;        //난이도 어려움
        else return 0;
    }

    private void LoadBin()
    {
        BinaryReader reader = new BinaryReader(File.Open("save.onladv", FileMode.OpenOrCreate));
        (char, string) ch;
        do
        {
            ch = SaveLoad.PartRead(reader);
            switch (ch.Item1)
            {
                case 'ㅁ':
                    df.map = ch.Item2;
                    break;
                case 'ㄱ':
                    df.exp = int.Parse(ch.Item2);
                    break;
                case 'ㅊ':
                    df.mhp = int.Parse(ch.Item2);
                    break;
                case 'ㅍ':
                    flags = ch.Item2;
                    break;
                case 'ㅇ':
                    df.caller = int.Parse(ch.Item2);
                    break;
                case 'ㄴ':
                    nanido = double.Parse(ch.Item2);
                    break;
                case 'ㅈ':
                    mgn = int.Parse(ch.Item2);
                    break;
                case 'ㅎ':
                    df.hp = int.Parse(ch.Item2);
                    break;
            }
        } while (ch.Item1 != ' ');
        reader.Close();
    }

    private void DecodeFlag(string s) { //상대적으로 복잡한 스트링 형태의 플래그를 정수형 배열로 해독
        string temp = "";
        int idx = -1;
        foreach(var c in s)
        {
            if (c != ',') temp += c;
            else
            {
                if (idx >= 0) df.PF[idx] = int.Parse(temp);
                idx++;
                temp = "";
            }
        }
    }

    private void Start()
    {
        LoadBin();
        if (nanido != -1) {
            DecodeFlag(flags);
            df.diff = Validity();
        }

        DisplayData2();
    }

    private void Update()
    {
        if (nanido != -1)
        {
            gogo.text = "이어하기";
            if (df.diff == 0)
            {
                gogo.text = "데이터 손상";
            }
        }
        else
        {
            gogo.text = "새로하기";
            dataDisp.text = "";
        }
    }

    public void Erase()
    {
        char c = counts.text.ToCharArray()[0];
        if (c != '1')
        {
            c--;
            counts.text = c.ToString();
        }
        else {
            counts.text = "5";
            counts.GetComponentInParent<Button>().gameObject.SetActive(false);
            BinaryWriter eraser = new BinaryWriter(File.Open("save.onladv", FileMode.Create));
            eraser.Close();
            ertext.text = "플레이 데이터가 삭제되었습니다.";
            nanido = -1;
        }
    }

    public void Load()
    {
        if (nanido != -1) 
        {
            if (df.diff == 0) {                                
                return;
            }
            //데이터를 가지고, 나는 사라지고 누군가한테는 전달해야 함. 세이브포인트 씬으로 이동
            df.Fill();
            Scenemover.MoveScene(df.map);
        }
        else
        {           
            gogo.text = "난이도를 선택하세요.";
            nanedo.SetActive(true);
        }
    }

    public void DfSelect(int level)
    {
        df.diff = level;
        df.NewFill();
        Scenemover.MoveScene("MyRoom");
    }

    public void Quit()
    {
        Application.Quit();        
    }
}
