using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    public int killframe;
    int state = 0;

    // Update is called once per frame
    void Update()
    {
        state++;
        if (state >= killframe) Destroy(gameObject);
    }
}
