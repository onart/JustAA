using UnityEngine;

public class JointBody : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite[] sprites;    //sprites[0]=초기값
    public Rigidbody2D rb2d;
    HingeJoint2D hj2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hj2d = GetComponent<HingeJoint2D>();
    }

    public void setTail(JointBody jb)
    {
        if (hj2d) hj2d.connectedBody = jb.rb2d;
    }

    public void setAngleLim(float min, float max)
    {
        hj2d.limits = new JointAngleLimits2D()
        {
            max = max,
            min = min
        };
    }

    public void setSp(int idx)
    {
        if (idx < 0) return;
        try {
            sr.sprite = sprites[idx];
        }
        catch (System.IndexOutOfRangeException)
        {
            return;
        }
    }

}
