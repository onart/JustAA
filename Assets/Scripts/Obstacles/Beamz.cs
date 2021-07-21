using UnityEngine;

public class Beamz : MonoBehaviour
{
    int mask = 0;

    public int range;
    public GameObject laser;
    public bool isRotating;     //제작자가 미리 정해두기. 회전 시 Update()에 angle2vector가 들어감
    float destination;
    Vector2 dir;

    void Start()
    {
        angle2vector();
        if (range == 0) mask += (1 << LayerMask.NameToLayer("Map"));
        mask += (1 << LayerMask.NameToLayer("Player"));
        mask += (1 << LayerMask.NameToLayer("Foreground"));
        mask += (1 << LayerMask.NameToLayer("Enemy"));
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating) angle2vector();
        RenderRay();
    }

    void RenderRay()
    {
        destination = Physics2D.Raycast(transform.position, dir, float.PositiveInfinity, mask).distance;
        if (destination == 0) destination = range;
        laser.transform.localScale = new Vector2(destination / 2.8f, laser.transform.localScale.y);
    }

    void angle2vector()
    {
        float ang = transform.eulerAngles.z - 90;
        laser.transform.eulerAngles = new Vector3(0, 0, ang);
        ang *= Mathf.Deg2Rad;
        dir = new Vector2(Mathf.Cos(ang), Mathf.Sin(ang));
    }
}
