using UnityEngine;

public class FoeHp : MonoBehaviour
{
    SpriteRenderer sr;
    public int alphaTime;      //투명도의 기준이 됨. 0.5초 정도 보여주고 서서히 숨김

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        alphaTime--;
        if (alphaTime <= 0)
        {
            sr.color = Color.white;
            alphaTime = 60;
            gameObject.SetActive(false);
        }
        else if (alphaTime <= 30)
        {
            sr.color = new Color(1, 1, 1, alphaTime * 0.03f);
        }
        else
        {
            sr.color = Color.white;     //alphaTime이 30 이상인 경우
        }
    }
}
