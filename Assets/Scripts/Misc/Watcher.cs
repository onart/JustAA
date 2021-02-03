using UnityEngine;

public class Watcher : MonoBehaviour    //플레이어를 해바라기처럼 봄
{
    public float d0;   //초기 각도(degree)
    Transform follow;

    void Start()
    {
        follow = FindObjectOfType<Player>().transform;
        d0 *= Mathf.Deg2Rad;
    }

    void Update()
    {
        Vector2 p = follow.position - transform.position + Vector3.up * 0.1f;
        float ang = Mathf.Atan(p.y / p.x) - d0;
        transform.eulerAngles = Vector3.forward * ang * Mathf.Rad2Deg;
        if (p.x < 0) transform.eulerAngles += Vector3.forward * 180;
    }
}
