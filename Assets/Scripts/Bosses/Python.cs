using System.Collections;
using UnityEngine;

public class Python : LinearJointStructure
{
    Transform tf;
    Vector2 head0;
    float relDeg;

    void Start()
    {
        joints = GetComponentsInChildren<JointBody>();
        for (int i = 0; i + 1 < joints.Length; i++)
        {
            joints[i].setTail(joints[i + 1]);
            joints[i].setAngleLim(lo, hi);
        }
        tf = FindObjectOfType<Player>().transform;
        head0 = joints[0].transform.position;
        StartCoroutine(Head0());
    }

    IEnumerator Head0()
    {
        joints[1].rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        joints[2].rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        for (int i = 1; i <= 100; i++)
        {
            joints[0].rb2d.MovePosition((head0 * i + head0 * (100 - i)) / 100);
            yield return new WaitForSeconds(0.01f);
        }
        //joints[0].rb2d.MovePosition(head0);
        yield return new WaitForEndOfFrame();
    }

    private void Update()
    {
        float dx = tf.position.x - joints[0].transform.position.x;
        float dy = tf.position.y - joints[0].transform.position.y;
        relDeg = Mathf.Atan(dy / dx) * Mathf.Rad2Deg;
        joints[0].rb2d.MoveRotation(Mathf.Clamp(relDeg, -35, 35));
        //joints[0].rb2d.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Snipe((Vector2)tf.position + Vector2.up));
        //joints[0].rb2d.MovePosition(joints[0].rb2d.position + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))/100);
    }

    public IEnumerator Snipe(Vector2 t)
    {
        joints[7].rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        var j0 = joints[0];
        j0.rb2d.constraints = RigidbodyConstraints2D.None;
        Vector2 h0 = j0.transform.position;
        j0.setSp(1);
        yield return new WaitForSeconds(1.0f / SysManager.difficulty);
        j0.setSp(2);
        j0.rb2d.MovePosition(t);
        yield return new WaitForSeconds(1.5f);
        Vector2 h1 = j0.transform.position;
        for (int i = 1; i <= 100; i++)
        {
            j0.rb2d.MovePosition((h0 * i + h1 * (100 - i)) / 100);
            yield return new WaitForSeconds(0.01f);
        }
        joints[7].rb2d.constraints = RigidbodyConstraints2D.None;
        j0.rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public IEnumerator Shake()
    {
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator Spit()
    {
        yield return new WaitForSeconds(1.0f);
    }

    public IEnumerator Flip()
    {
        yield return new WaitForSeconds(1.0f);
    }

}
