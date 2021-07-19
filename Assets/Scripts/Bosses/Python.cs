using System.Collections;
using UnityEngine;

public class Python : Boss
{
    JointBody[] joints;
    public float hi, lo; // 공통 각도제한(max, min 순서)

    float delay;
    Vector2 head0, dest;
    int rayMask;
    bool busy;
    public GameObject warnRay;
    public GameObject crab, stone, gliq;

    public Cinemachine.CinemachineImpulseSource imsr;

    protected override void St()
    {
        busy = false;
        delay = 9 / SysManager.difficulty;
        maxHp = 100000000;
        hp = maxHp;
        at.enabled = false;
        exp = 0;
        base.St();
        rayMask = 1 << LayerMask.NameToLayer("Map");
        joints = GetComponentsInChildren<JointBody>();
        dmgPos = joints[0].transform;
        for (int i = 0; i + 1 < joints.Length; i++)
        {
            joints[i].setTail(joints[i + 1]);
            joints[i].setAngleLim(lo, hi);
        }
        head0 = joints[0].transform.position;
        dest = head0;
        Invoke(nameof(Act), delay);
    }

    private void Update()
    {
        if (dest.magnitude != 0)
        {
            joints[0].rb2d.MovePosition((dest + joints[0].rb2d.position * 19) / 20);
        }
        if (!busy)
        {
            Seek(joints[0].transform);
            if (dx > 0) joints[0].rb2d.MoveRotation(0);
            else joints[0].rb2d.MoveRotation(Mathf.Clamp(relDeg, -70, 70));
            /*
            if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Snipe(dx, dy));
            if (Input.GetKeyDown(KeyCode.C)) { StartCoroutine(Shake()); }
            if (Input.GetKeyDown(KeyCode.B)) { StartCoroutine(Spit()); }*/
        }
    }

    void Act()
    {
        if(!busy)
        {
            if (relDeg > 0 && relDeg < 60)
            {
                switch (Random.Range(0, 10))
                {
                    case 0:
                    case 1:
                        StartCoroutine(Shake());
                        break;
                    case 2:
                    case 3:
                        StartCoroutine(Spit());
                        break;
                    default:
                        StartCoroutine(Snipe());
                        break;
                }
            }
            else
            {
                StartCoroutine(Shake());
            }
        }
        Invoke(nameof(Act), delay);
    }

    IEnumerator Snipe()
    {        
        dest = Vector2.zero;
        busy = true;
        var j0 = joints[0];
        j0.rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        var rh = Physics2D.Raycast(joints[0].transform.position, new Vector2(dx, dy), float.PositiveInfinity, rayMask).point;
        j0.setSp(1);
        var ray = Instantiate(warnRay);
        ray.GetComponent<WarnRay>().t = 1.0f / SysManager.difficulty;
        ray.transform.localPosition = j0.transform.position;
        ray.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan(dy / dx) * Mathf.Rad2Deg);
        ray.transform.localScale = new Vector2(-5, 10);
        yield return new WaitForSeconds(0.5f / SysManager.difficulty);
        //j0.rb2d.MovePosition(rh);
        j0.rb2d.velocity = (rh - j0.rb2d.position) * 50;
        at.enabled = true;
        j0.setSp(0);
        yield return new WaitForSeconds(1f);
        at.enabled = false;
        busy = false;
        j0.rb2d.constraints = RigidbodyConstraints2D.None;
        dest = head0;
    }

    public IEnumerator Shake()
    {
        busy = true;
        var tail = joints[14].rb2d;
        tail.MoveRotation(80);
        yield return new WaitForSeconds(0.3f);
        tail.AddTorque(-10000);
        yield return new WaitForSeconds(0.2f);
        imsr.GenerateImpulse();
        if (Player.inst.onground) {
            Player.inst.GetHit(9 * SysManager.difficulty, 2);
            prb2d.AddForce(Vector2.up * 200);
        }
        FallDown();
        busy = false;
    }

    void FallDown()
    {
        for(int i = 0; i < SysManager.difficulty * 5; i++)
        {
            var x = Random.Range(-5.5f, 3.5f);
            var v0 = Random.Range(-SysManager.difficulty * 2, 0f);
            var st = Instantiate(stone);
            st.transform.localScale = Vector2.one * Random.Range(0.15f, 0.3f);
            st.transform.position = new Vector2(x, -2.5f);
            st.GetComponent<Rigidbody2D>().velocity = new Vector2(0, v0);
        }
        for (int i = 0; i < SysManager.difficulty; i++)
        {
            if (Random.Range(0, 10) >= SysManager.difficulty) continue;
            var x = Random.Range(-5.5f, -1.5f);
            var v0 = Random.Range(-SysManager.difficulty, 0f);
            var st = Instantiate(crab);
            st.transform.position = new Vector2(x, -2.5f);
            st.GetComponent<Rigidbody2D>().velocity = new Vector2(0, v0);
        }
    }

    IEnumerator Spit()
    {
        busy = true;
        dest = new Vector2(0.6f, -5);
        var j0 = joints[0];
        j0.rb2d.MoveRotation(-30);        
        j0.setSp(1);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < SysManager.difficulty * 3; i++)
        {
            var g = Instantiate(gliq);
            g.transform.position = j0.transform.position;
            g.transform.localScale = Vector2.one * Random.Range(0.5f, 1);
            g.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-8, -1), Random.Range(1.5f, 4.0f));
        }
        j0.setSp(0);
        j0.rb2d.constraints = RigidbodyConstraints2D.None;
        busy = false;
        dest = head0;
    }

    public IEnumerator still(float time)
    {
        for (int i = 0; i + 1 < joints.Length; i++)
        {
            joints[i].rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        yield return new WaitForSeconds(time);
        for (int i = 0; i + 1 < joints.Length; i++)
        {
            joints[i].rb2d.constraints = RigidbodyConstraints2D.None;
        }
        joints[12].rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    protected override IEnumerator OnZero()
    {
        HPChange(0x1000000);
        yield break;
    }

    protected override void HPChange(int delta)
    {
        base.HPChange(delta);
        switch ((maxHp - hp) / 200)
        {
            case 1:
                if (delay == 9 / SysManager.difficulty) {
                    delay = 6 / SysManager.difficulty;
                    //StartCoroutine(Snipe()); 고정각/즉발로 추가
                }
                break;
            case 2:
                if (delay == 6 / SysManager.difficulty) {
                    delay = 4 / SysManager.difficulty;
                    //StartCoroutine(Snipe()); 고정각/즉발로 추가
                }
                break;
            case 3:
                if (delay == 4 / SysManager.difficulty) {
                    delay = 3 / SysManager.difficulty;
                    //StartCoroutine(Snipe()); 고정각/즉발로 추가
                }
                break;
            default:
                break;
        }
    }
}
