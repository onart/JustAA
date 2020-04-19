using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIntro : MonoBehaviour
{
    public string mapName;
    TalkManager tm;

    void Start()
    {
        tm = FindObjectOfType<TalkManager>();
    }
    private void Update()
    {
        tm.NoteFor(mapName);
        enabled = false;
    }
}
