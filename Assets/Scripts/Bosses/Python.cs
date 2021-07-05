using System.Collections;
using UnityEngine;

public class Python : LinearJointStructure
{
    Transform tf;
    Vector2 head0;
    float relDeg;
    int rayMask;
    bool busy;
    public GameObject warnRay;

    void Start()
    {
        busy = false;
        rayMask = 1 << LayerMask.NameToLayer("Map");
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
        busy = true;
        joints[1].rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        joints[2].rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
        for (int i = 1; i <= 100; i++)
        {
            joints[0].rb2d.MovePosition((head0 * i + joints[0].rb2d.position * (100 - i)) / 100);
            yield return new WaitForSeconds(0.01f);
        }
        //joints[0].rb2d.MovePosition(head0);
        yield return new WaitForEndOfFrame();
        busy = false;
    }

    private void Update()
    {
        float dx = tf.position.x - joints[0].transform.position.x;
        float dy = tf.position.y - joints[0].transform.position.y;
        relDeg = Mathf.Atan(dy / dx) * Mathf.Rad2Deg;
        if (dx > 0) joints[0].rb2d.MoveRotation(0);
        else joints[0].rb2d.MoveRotation(Mathf.Clamp(relDeg, -60, 60));
        if (!busy)
        {
            joints[0].rb2d.MovePosition((head0 + joints[0].rb2d.position * 19) / 20);
            if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Snipe(dx, dy));
        }
    }

    public IEnumerator Snipe(float dx, float dy)
    {
        dy++;
        busy = true;
        joints[1].rb2d.constraints = RigidbodyConstraints2D.None;
        joints[2].rb2d.constraints = RigidbodyConstraints2D.None;
        var j0 = joints[0];
        j0.rb2d.constraints = RigidbodyConstraints2D.None;
        var rh = Physics2D.Raycast(joints[0].transform.position, new Vector2(dx, dy), float.PositiveInfinity, rayMask);
        j0.setSp(1);
        var ray = Instantiate(warnRay, j0.transform);
        ray.transform.localPosition = Vector2.zero;
        ray.transform.parent = null;
        ray.transform.localScale = new Vector2(-5, 10);
        yield return new WaitForSeconds(1.0f / SysManager.difficulty);        
        j0.rb2d.MovePosition(rh.point);
        j0.setSp(0);
        yield return new WaitForSeconds(1f);
        Vector2 h1 = j0.transform.position;
        for (int i = 1; i <= 30; i++)
        {
            j0.rb2d.MovePosition((head0 * i + h1 * (30 - i)) / 30);
            yield return new WaitForSeconds(0.01f);
        }
        busy = false;
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
