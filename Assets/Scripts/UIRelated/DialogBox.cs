using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogBox : MonoBehaviour
{
    //private int charcount = (int)BaseSet.Chars.CHARCOUNT;
    private int facecount = (int)BaseSet.Exprs.FACECOUNT;

    public bool ended; //대화 출력이 끝났는지
    public Image face;
    public TextMeshProUGUI contin;  //"계속하려면 ~를 누르세요"

    AudioSource aus;
    TextMeshProUGUI char_name, content;
    char[] con;
    int txt, unitfrm;    //현재 몇번째 글자인지, 글자 출력 속도 조정용 변수

    public Sprite[] face_list = new Sprite[(int)BaseSet.Chars.CHARCOUNT * (int)BaseSet.Exprs.FACECOUNT];    
    private void Awake()
    {
        aus = GetComponent<AudioSource>();
        char_name = GetComponentsInChildren<TextMeshProUGUI>()[0];
        content = GetComponentsInChildren<TextMeshProUGUI>()[1];
        ended = true;   //처음엔 대화 중이 아니니까
    }
    /*
     InitBox(캐릭터, 표정, 대사);로 대화상자 내용만 바꿈.
    */

    private void Update()       //글자마다 따로 출력
    {
        if (!ended && !SysManager.menuon && unitfrm % 2 == 0)     // % 오른쪽 숫자만큼 느려짐.
        {
            content.text += con[txt];
            if (!con[txt].Equals(' ')) aus.Play();       //공백에선 타이핑 소리 출력 안 함
            txt++;
            if (txt == con.Length) ended = true;
        }
        unitfrm++;
    }

    public void InitBox(BaseSet.Chars c, BaseSet.Exprs e, string cont)
    {
        if (c == BaseSet.Chars.CHARCOUNT) {     //포트레이트와 이름이 필요 없는 경우.
            face.color = Color.clear;
            char_name.text = "";
        }
        else {                                  //포트레이트와 이름이 존재하는 경우.
            int ch = (int)c;
            int ex = (int)e;
            face.color = Color.white;
            face.sprite = face_list[ch * facecount + ex];
            char_name.text = BaseSet.names[ch];
        }
        content.text = "";
        txt = 0;
        unitfrm = 0;
        ended = false;
        con = cont.ToCharArray();
        contin.SetText("(" + SysManager.keymap["상호작용"] + "키로 계속)");
    }

    public void InstantInitBox(string cont)
    {
        //출력 중 한 번 더 누르면 동시출력
        ended = true;
        content.text = cont;
    }

}
