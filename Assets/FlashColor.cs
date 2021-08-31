using UnityEngine;

public class FlashColor : MonoBehaviour
{
    public Color[] c;
    public int speed;

    SpriteRenderer sr;
    int idx, div;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        div = c.Length;
        if (speed <= 0) speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = c[(idx++ / speed) % div];
    }
}
