using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    const int CHARGES = 2;
    // Start is called before the first frame update
    Player p;
    Animator anim;
    Rigidbody2D rb2d;
    SpriteRenderer sr;

    float[] hold= new float[CHARGES];

    string[] keys = { "공격", "특수1" };
    //각각 박치기, 피하기

    void Start()
    {
        p = GetComponent<Player>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < CHARGES; i++)
        {
            holdFor(i);
        }
    }

    void holdFor(int k)
    {
        if (!skillOn(k))
        {
            return;
        }
        string key = keys[k];
        if (Input.GetKey(SysManager.keymap[key]))
        {
            if (hold[k] < 1)
            {
                hold[k] += Time.deltaTime;
            }
            else
            {
                if (k == 0) sr.color = new Color(1, 0.8f, 0.8f);
                else if (k == 1) sr.color = new Color(0.8f, 0.8f, 1);
            }
        }
        else if (Input.GetKeyUp(SysManager.keymap[key]))
        {
            if (hold[k] >= 1)
            {
                if (k == 0) anim.SetInteger("COMBO", 21);
                else if (k == 1) { anim.SetTrigger("RUSH"); rb2d.MovePosition(rb2d.position + Vector2.right * Input.GetAxisRaw("Horizontal"));}
            }
            hold[k] = 0;
            sr.color = Color.white;
        }
    }

    bool skillOn(int k)
    {
        switch (k)
        {
            case 0:
                return (p.FLAGS[(int)BaseSet.Flags.SKILLS] >> 1) % 2 == 1;
            case 1:
                return (p.FLAGS[(int)BaseSet.Flags.SKILLS] >> 2) % 2 == 1;
            default:
                return false;
        }
    }

}
