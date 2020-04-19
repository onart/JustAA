using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

//저장 위치는 레지스트리 편집기->HKEY_CURRENT_USER->SOFTWARE->UNITY->UNITYEDITOR->회사이름->게임이름
public class Titlemanager : MonoBehaviour
{
    public TextMeshProUGUI ertext, gogo, counts, dataDisp;
    public DataFiller df;
    public GameObject nanedo;
    public static int nando = -1;

    //저장된 데이터 표시 함수. 저장 맵, 플레이타임, 난이도, HP, EXP,
    void DisplayData()
    {
        string difficulty = "";
        nando = Validity();
        switch (nando)
        {
            case 1:
                difficulty = "쉬움";
                break;
            case 2:
                difficulty = "보통";
                break;
            case 3:
                difficulty = "어려움";
                break;
            default:
                dataDisp.SetText("데이터가 손상된 것 같습니다. 데이터를 지워 주세요.\n");
                return;
        }
        int pt = PlayerPrefs.GetInt("PlayTime");
        //Debug.Log(pt);
        int hp = PlayerPrefs.GetInt("CurrentHp");
        int mhp = PlayerPrefs.GetInt("MHP");
        int exp = PlayerPrefs.GetInt("EXP");

        string map = PlayerPrefs.GetString("Scene");
        string time = (int)(pt / 60) + "분" + pt % 60 + "초";
        string hps = hp + "/" + mhp;

        dataDisp.SetText(map + ' ' + time + '\n' + hps + '\n' + exp + "xp\n" + difficulty);
    }

    void DisplayData2()
    {
        BinaryReader reader = new BinaryReader(File.Open("onladv.sav", FileMode.OpenOrCreate));
        string map = reader.ReadString();
        int mhp = reader.ReadInt32();
        int hp = reader.ReadInt32();
        reader.ReadInt32();
        int exp = reader.ReadInt32();
        int difficulty = reader.ReadInt32();
        reader.Close();
        string hps = hp + "/" + mhp;
        dataDisp.SetText(map + ' ' + '\n' + hps + '\n' + exp + "xp\n" + difficulty);
    }

    int Validity()  //난이도 숫자의 저장 데이터의 유효성 판단. 역연산 후 절대오차가 작으면 통과한다.
    {
        int diff = 2;
        for (int i = 0; i < (int)BaseSet.Flags.FLAGCOUNT; i++)
        {
            diff += PlayerPrefs.GetInt("Fl" + i, 0);                           
        }
        float diff2 = Mathf.Log(diff, PlayerPrefs.GetFloat("난이도")) - 1;
        diff2 *= PlayerPrefs.GetInt("매직넘버");
        Debug.Log(diff2);
        if (Mathf.Abs(diff2 - 1) < 0.01f) return 1;             //난이도 쉬움
        else if (Mathf.Abs(diff2 - 2) < 0.01f) return 2;        //난이도 보통
        else if (Mathf.Abs(diff2 - 3) < 0.01f) return 3;        //난이도 어려움
        else return 0;  //세이브데이터 손상을 알림.
    }

    private void Start()
    {
        DisplayData2();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Exist"))
        {
            gogo.text = "이어하기";
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
            ertext.text = "플레이 데이터가 삭제되었습니다.";
            PlayerPrefs.DeleteAll(); 
        }
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Exist"))
        {
            //데이터를 가지고, 나는 사라지고 누군가한테는 전달해야 함. 세이브포인트 씬으로 이동
            df.nando = nando;
            df.Fill();
            Scenemover.MoveScene(PlayerPrefs.GetString("Scene"));
        }
        else
        {           
            gogo.text = "난이도를 선택하세요.";
            nanedo.SetActive(true);
        }
    }

    public void DfSelect(int level)
    {
        df.nando = level;
        df.NewFill();
        Scenemover.MoveScene("MyRoom");
    }

    public void Quit()
    {
        Application.Quit();        
    }
}
