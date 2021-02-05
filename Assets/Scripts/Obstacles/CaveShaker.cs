using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CaveShaker : MonoBehaviour
{
    Player p;
    Transform gauge;
    float scale = 1;
    public Cinemachine.CinemachineImpulseSource imsr;

    void Start()
    {
        p = FindObjectOfType<Player>();
        gauge = GetComponent<Image>().transform;
        StartCoroutine(nameof(Act));
    }

    IEnumerator Act()
    {
        while (true)
        {
            if (scale > 0) scale -= .01f;
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
    }

    void camShake()
    {
        //효과음
        imsr.GenerateImpulse();
    }
}
