using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    //저장 위치는 레지스트리 편집기->HKEY_CURRENT_USER->SOFTWARE->UNITY->UNITYEDITOR->회사이름->게임이름
    //암호화 등 치트방지는 나중에 추가
    public Player pl;    
    public void Save(Scene s, int i)
    {
        float pt = Time.time;
        float tpt = PlayerPrefs.GetFloat("PlayTime", 0) + pt;
        PlayerPrefs.SetString("Scene", s.name);
        PlayerPrefs.SetInt("MHP", pl.MHP);
        PlayerPrefs.SetInt("CurrentHp", pl.HP);
        PlayerPrefs.SetInt("Exist", 1);
        PlayerPrefs.SetInt("Caller",i);
        PlayerPrefs.SetInt("EXP", pl.exp);
        PlayerPrefs.SetInt("PlayTime", (int)tpt);
        PlayerPrefs.SetInt("매직넘버", Mathf.CeilToInt(pt)); //이건 난이도 변경 못하게 걸어잠그는 데 쓸 거다.
        int diff = 0;   //난이도 관련 저장
        for (int j = 0; j < (int)BaseSet.Flags.FLAGCOUNT; j++) 
        {
            diff += pl.FLAGS[j];
            PlayerPrefs.SetInt("Fl"+j.ToString(), pl.FLAGS[j]); //이벤트 플래그 저장
        }
        PlayerPrefs.SetFloat("난이도", Mathf.Pow(diff + 2, 1 + SysManager.difficulty / Mathf.CeilToInt(pt)));   //이건 암호화된 난이도 변수다.        
        SysManager.KeyMapSave();
        PlayerPrefs.Save();
        //실험용 따로 저장
        BinaryWriter writer = new BinaryWriter(File.Open("onladv.sav", FileMode.Create));
        writer.Write(s.name);
        writer.Write(pl.MHP);
        writer.Write(pl.HP);
        writer.Write(i);
        writer.Write(pl.exp);
        writer.Write(SysManager.difficulty);
        writer.Close();
    }
    public void ToScene(string sc)
    {
        SceneManager.LoadScene(sc);
        SysManager.menuon = false;
    }

}
