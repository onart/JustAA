using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//타이틀에서 시작을 눌렀을 때 등 캐릭터를 새로 만들어내는 요소.
public class Helper : MonoBehaviour
{
    public GameObject g;    //씬 고정 객체의 Prefab
    public int caller;      //플레이어를 위치로 불러오는 데 필요한 구분자.
    void Awake()
    {
        if (!FindObjectOfType<Maintainable>()) {
            if (!PlayerPrefs.HasKey("Caller"))
            {
                //이러한 구조 때문에 첫 맵에는 절대 일반 시작 포인트와 USB를 같이 두지 말 것. 이러면 USB에서 시작할 수도 있음
                g = Instantiate(g);
                Player p = g.GetComponentInChildren<Player>();
                p.transform.position = transform.position;
            }
            else if(PlayerPrefs.GetInt("Caller") == caller)
            {
                g = Instantiate(g);
                Player p = g.GetComponentInChildren<Player>();
                p.transform.position = transform.position;
            }
            //이와 다른 케이스도 있으나... 테스트 이외의 방법으로 정상적 진입은 불가능하므로 그냥 두자
        }
    }
}
