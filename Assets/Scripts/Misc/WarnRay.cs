using System.Collections;
using UnityEngine;

public class WarnRay : MonoBehaviour
{
    public float t, d;  //잔존 시간, 수축 시간(<t)
    void Start()
    {
        StartCoroutine(thinner());
    }

    IEnumerator thinner()
    {
        yield return new WaitForSeconds(t - d);
        for (; d > 0; d -= 0.03f)
        {
            transform.localScale = transform.localScale - new Vector3(0, transform.localScale.y / 3, 0);
            yield return new WaitForSeconds(0.03f);
        }
        Destroy(gameObject);
    }
}
