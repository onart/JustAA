using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIntro : MonoBehaviour
{
    public string mapName;
    TalkManager tm;
    // Start is called before the first frame update
    void Start()
    {
        tm = FindObjectOfType<TalkManager>();
    }
    private void Update()
    {
        tm.NoteFor(mapName);
        Destroy(this.gameObject);
    }
}
