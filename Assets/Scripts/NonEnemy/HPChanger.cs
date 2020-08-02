using UnityEngine;

public abstract class HPChanger : MonoBehaviour  //체력을 회복시키거나 깎는 상대 물체에 부착
{
    public int delta;  //변화량

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var p = col.gameObject.GetComponent<Player>();
            Act(p);
        }
    }

    protected abstract void Act(Player p);
}
