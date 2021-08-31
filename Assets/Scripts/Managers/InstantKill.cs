using UnityEngine;

public class InstantKill : MonoBehaviour    // 애니메이터에서 호출하더라.. (prefab HIT 참조)
{
    public void killEv()
    {
        Destroy(gameObject);
    }
}
