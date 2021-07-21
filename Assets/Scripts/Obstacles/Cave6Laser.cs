using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave6Laser : MonoBehaviour
{
    public Transform[] beamp;
    float phase, lim;
    float delta;
    // Start is called before the first frame update
    void Start()
    {
        phase = 0;
        lim = 360;
        delta = 0.001f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SysManager.forbid) return;
        phase += delta;
        if (phase >= 1 || phase <= 0) delta = -delta;
        float baseA = 0;
        foreach(var b in beamp)
        {
            b.rotation = Quaternion.Euler(0, 0, baseA + Mathf.Lerp(0, lim, phase));
            baseA += 90;
        }
    }

    public void changeLim(float lim)
    {
        this.lim = lim;
        phase *= (this.lim / lim);
    }
}
