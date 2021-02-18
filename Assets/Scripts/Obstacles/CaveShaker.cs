using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CaveShaker : MonoBehaviour
{
    Player p;
    Transform gauge;
    float scale = 1;
    CaveCrab[] crabs;
    public Cinemachine.CinemachineImpulseSource imsr;
    readonly int crabForce = 10000 + 2000 * SysManager.difficulty;

    void Start()
    {
        p = FindObjectOfType<Player>();
        gauge = GetComponent<Image>().transform;
        StartCoroutine(nameof(Act));
        crabs = FindObjectsOfType<CaveCrab>();
    }

    IEnumerator Act()
    {
        while (true)
        {
            if (scale > 0) scale -= .006f * SysManager.difficulty;
            else if (scale <= 0)
            {
                shake();
                scale = 1;
            }
            gauge.localScale = new Vector2(scale, 1);
            yield return new WaitForSeconds(0.03f);
        }
    }

    void shake()
    {
        camShake();
        if (p.onground)
        {
            p.rb2d.AddForce(new Vector2(0, 200));
            p.GetHit(0, 2);
        }
        foreach(var cr in crabs)
        {
            if (cr) cr.GetHit(0, new Vector2(0, crabForce));
        }
    }

    void camShake()
    {
        //효과음
        imsr.GenerateImpulse();
    }
}
