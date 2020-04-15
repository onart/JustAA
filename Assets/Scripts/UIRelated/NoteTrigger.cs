using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
    public int noteID;
    static TalkManager tm;
    private void Start()
    {
        if (tm == null) tm = FindObjectOfType<TalkManager>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            tm.NoteFor(BaseSet.Notes[noteID]);
        }
    }
}
