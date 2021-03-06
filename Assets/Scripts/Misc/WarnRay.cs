using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnRay : MonoBehaviour
{
    public float t;  //잔존 시간
    void Start()
    {
        StartCoroutine(thinner());
    }

    IEnumerator thinner()
    {
        for (; t > 0; t -= 0.03f)
        {
            transform.localScale = transform.localScale - new Vector3(0, transform.localScale.y / 3, 0);
            yield return new WaitForSeconds(0.03f);
        }
        Destroy(gameObject);
    }
}
