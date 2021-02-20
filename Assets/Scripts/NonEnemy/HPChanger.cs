using UnityEngine;

public abstract class HPChanger : MonoBehaviour  //체력을 회복시키거나 깎는 상대 물체에 부착
{
    public int delta;  //변화량
    protected static Player p;
    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!p) p = col.gameObject.GetComponent<Player>();
            Act();
        }
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!p) p = col.gameObject.GetComponent<Player>();
            Act();
        }
    }

    protected abstract void Act();
}
