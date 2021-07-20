using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave6Laser : MonoBehaviour
{
    public Transform[] beamp;
    float angle, lim;
    // Start is called before the first frame update
    void Start()
    {
        angle = 0;
        lim = 90;
    }

    // Update is called once per frame
    void Update()
    {
        if (SysManager.forbid) return;
        angle += 0.002f;
        int baseA = 0;
        foreach(var b in beamp)
        {
            b.rotation = Quaternion.Euler(0, 0, baseA + lim * Mathf.Sin(angle));
            baseA += 90;
        }
    }

    public void changeLim(int lim)
    {
        this.lim = lim;
    }
}
