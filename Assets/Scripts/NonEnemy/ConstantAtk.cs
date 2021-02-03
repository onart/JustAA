using UnityEngine;

public class ConstantAtk : Attacker
{
    public float period;
    Collider2D tr;

    private void Start()
    {
        tr = GetComponent<Collider2D>();
        if (period == 0) period = 1;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        tr.enabled = false;
        Invoke("regen", period);
    }

    void regen()
    {
        tr.enabled = true;
    }

}
