using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    IEnumerator printBox()
    {
        foreach (char lt in con)
        {
            while (SysManager.menuon) yield return new WaitForSecondsRealtime(0.02f);
            content.text += lt;
            if (!lt.Equals(' ')) aus.Play();
            yield return new WaitForSecondsRealtime(0.02f);
            if (ended) break;
        }
        ended = true;
    }

    public void InitBox(BaseSet.Chars c, BaseSet.Exprs e, string cont)
    {
        if ((int)c >= (int)BaseSet.Chars.CHARCOUNT)     //포트레이트가 없는 경우.
        {
            face.color = Color.clear;
            char_name.text = BaseSet.names[(int)c];
        }
        else
        {                                  //포트레이트와 이름이 존재하는 경우.
            int ch = (int)c;
            int ex = (int)e;
            face.color = Color.white;
            face.sprite = face_list[ch * facecount + ex];
            char_name.text = BaseSet.names[ch];
        }
        content.text = "";
        ended = false;
        con = cont.ToCharArray();
        contin.SetText("(" + SysManager.keymap["상호작용"] + "키로 계속)");
        StartCoroutine(printBox());
    }

    public void InstantInitBox(string cont)
    {
        //출력 중 한 번 더 누르면 전체출력
        StopCoroutine(printBox());
        ended = true;
        content.text = cont;
    }

}
