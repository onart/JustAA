using UnityEngine;

public class MapRoad : Door
{
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ObjAct();
        }
    }
}
