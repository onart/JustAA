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

    public float coef = 1;    //주기 계수, 기본=1, 높을수록 주기가 짧아짐
    float dsc;

    void Start()
    {
        p = FindObjectOfType<Player>();
        gauge = GetComponent<Image>().transform;
        crabs = FindObjectsOfType<CaveCrab>();
        if (DataFiller.load_complete) { 
            dsc = .006f * SysManager.difficulty * coef; 
            StartCoroutine(nameof(Act)); 
        }
        else { Invoke(nameof(Start), 0.03f); }
    }

    IEnumerator Act()
    {
        while (true)
        {
            if (scale > 0) scale -= dsc;
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
        foreach (var cr in crabs)
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
