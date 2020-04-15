using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SaveLoad : MonoBehaviour
{
    //저장 위치는 레지스트리 편집기->HKEY_CURRENT_USER->SOFTWARE->UNITY->UNITYEDITOR->회사이름->게임이름
    //암호화 등 치트방지는 나중에 추가
    public Player pl;

    public void Save(Scene s, int i)
    {
        PlayerPrefs.SetString("Scene", s.name);
        PlayerPrefs.SetInt("MHP", pl.MHP);
        PlayerPrefs.SetInt("CurrentHp", pl.HP);
        PlayerPrefs.SetInt("Exist", 1);
        PlayerPrefs.SetInt("Caller",i);
        for (int j = 0; j < (int)BaseSet.Flags.FLAGCOUNT; j++) 
        {
            PlayerPrefs.SetInt("Fl"+j.ToString(), pl.FLAGS[j]); //이벤트 플래그 저장
        }
        PlayerPrefs.Save();
    }
    public void ToScene(string sc)
    {
        SceneManager.LoadScene(sc);
        SysManager.menuon = false;
    }

}
