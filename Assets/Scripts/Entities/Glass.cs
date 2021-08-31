using UnityEngine;

public class Glass : Hitable
{
    public int hp;
    public Python py;
    AudioSource br;

    void Start()
    {
        br = GetComponentInParent<AudioSource>();
    }

    private void OnDestroy()
    {
        py.shorten();
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
