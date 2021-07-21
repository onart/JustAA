using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : Hitable
{
    public int hp;
    AudioSource br;

    void Start()
    {
        br = GetComponentInParent<AudioSource>();
    }

    private void OnDestroy()
    {
        if (br && br.isActiveAndEnabled) br.Play();
    }

    public override void Act(Vector2 force)
    {
        br.Play();
        if (--hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
