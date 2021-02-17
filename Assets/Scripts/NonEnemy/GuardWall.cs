using UnityEngine;

public class GuardWall : MonoBehaviour  //몹을 없애야 제거되는 벽
{
    public Enemy[] guards;
    public int req; //없애야 하는 몹 수

    void Update()
    {
        int i = guards.Length - req;
        foreach (var g in guards)
        {
            if (g && --i < 0) return;
        }
        //효과음
        Destroy(gameObject);
    }
}
