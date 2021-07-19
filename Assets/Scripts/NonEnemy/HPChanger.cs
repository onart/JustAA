using UnityEngine;

public abstract class HPChanger : MonoBehaviour  //체력을 회복시키거나 깎는 상대 물체에 부착
{
    public int delta;  //변화량
    public float cooldown = 0;
    float lastH = 0;

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (enabled && col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Time.time > lastH) {
                lastH = Time.time + cooldown;
                Act();
            }
        }
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (enabled && col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Time.time > lastH)
            {
                lastH = Time.time + cooldown;
                Act();
            }
        }
    }

    protected abstract void Act();
}
