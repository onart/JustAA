using UnityEngine;

public class Hitable : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    protected Collider2D c2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        c2d = GetComponent<Collider2D>();
    }

    public virtual void Act(Vector2 force)
    {
        rb2d.AddForce(force);
    }
}
