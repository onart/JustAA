using UnityEngine;

public class GunProj : Attacker // 적측 발사체
{
    public float span;  //총알의 수명(초)

    private void Start()
    {
        Invoke("ProjHit", span);
        float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Laun.bulletSpeed;
    }

    private new void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        Invoke("ProjHit", 0.05f);
    }

    void ProjHit()
    {
        Destroy(gameObject);
    }
}
