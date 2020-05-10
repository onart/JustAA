using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    //저장 위치는 레지스트리 편집기->HKEY_CURRENT_USER->SOFTWARE->UNITY->UNITYEDITOR->회사이름->게임이름
    //암호화 등 치트방지는 나중에 추가
    
    public Player pl;
    string mapName;

    BinaryWriter writer;

    public void Save(Scene s, int caller)
    {
        mapName = s.name;

        List<char> c = new List<char> { 'ㄴ', 'ㅁ', 'ㅊ', 'ㅎ', 'ㅇ', 'ㄱ', 'ㅈ', 'ㅍ' };
        //ㄴ(난이도), ㅁ(맵) ㅊ(최대HP), ㅎ(현재HP), ㅇ(저장장치 위치), ㄱ(경험치), ㅈ(매직넘버), ㅍ(플래그)
        writer = new BinaryWriter(File.Open("onladv.sav", FileMode.Create));
        while (c.Count != 0)
        {
            int rd = Random.Range(0, c.Count);
            PartSave(c[rd], caller);
            c.RemoveAt(rd);
        }
        writer.Close();

        SysManager.KeyMapSave();
        PlayerPrefs.Save();
    }
    public void ToScene(string sc)
    {
        SceneManager.LoadScene(sc);
        SysManager.menuon = false;
    }

    void PartSave(char c, int caller)
    {
        writer.Write(c);
        switch (c)
        {
            case 'ㅁ':
                writer.Write(mapName);
                return;
            case 'ㄱ':
                writer.Write(pl.exp);
                return;
            case 'ㅊ':
                writer.Write(pl.MHP);
                return;
            case 'ㅍ':
                foreach(int flag in pl.FLAGS)
                {
                    writer.Write(flag);
                }
                writer.Write(-1);   //플래그 수는 업데이트 때문에 유동적이므로 종결에 표시가 필요함
                return;
            case 'ㅇ':
                writer.Write(caller);
                return;
            case 'ㄴ':
                int pt = Mathf.CeilToInt(Time.unscaledTime);
                int dff = 2;
                foreach (int flag in pl.FLAGS)
                {
                    dff += flag;
                }                
                double diff = Mathf.Pow(dff, 1 + (float)SysManager.difficulty / pt);
                writer.Write(diff);
                return;
            case 'ㅈ':
                int mgn = Mathf.CeilToInt(Time.unscaledTime);
                mgn += (mapName[0] - 'A');
                mgn += pl.MHP;
                writer.Write(mgn);
                return;
            case 'ㅎ':
                writer.Write(pl.HP);
                return;
        }
    }
    
    public static (char,string) PartRead(BinaryReader reader) //현재 8회 부르면 EOF
    {
        if (!(reader.PeekChar() > 0)) return (' ', null);   
        //비어 있는 경우 뭐 비어 있는 거지(이건 세이브 파일이 없거나/저장 요소가 새로 추가되었을 때를 위한 처리)
        char c = reader.ReadChar();
        switch (c)
        {
            case 'ㅁ':
                string map = reader.ReadString();
                return (c, map);
            case 'ㄱ':
            case 'ㅊ':
            case 'ㅇ':
            case 'ㅈ':
            case 'ㅎ':
                int ival = reader.ReadInt32();
                return (c, ival.ToString());
            case 'ㄴ':
                double pt = reader.ReadDouble();
                return (c, pt.ToString());
            case 'ㅍ':
                string ev = "";
                int tempFlag = 0;
                while (tempFlag != -1) {
                    ev += tempFlag + ",";
                    tempFlag = reader.ReadInt32();
                }
                return (c, ev);
            default:
                return (c, "error");
        }
    }


}
