using UnityEngine;
using UnityEngine.UI;
using TMPro;
//저장 위치는 레지스트리 편집기->HKEY_CURRENT_USER->SOFTWARE->UNITY->UNITYEDITOR->회사이름->게임이름
public class Titlemanager : MonoBehaviour
{
    public TextMeshProUGUI ertext, gogo, counts;
    public DataFiller df;

    private void Update()
    {
        if (PlayerPrefs.HasKey("Exist"))
        {
            gogo.text = "이어하기";
        }
        else
        {
            gogo.text = "새로하기";
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
            df.Fill();
            Scenemover.MoveScene(PlayerPrefs.GetString("Scene"));
        }
        else
        {
            //첫 씬으로 이동
            Destroy(df.gameObject);
            Scenemover.MoveScene("MyRoom");
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
