using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maintainable : MonoBehaviour
{
    private void Awake()

    {

        var obj = FindObjectsOfType<Maintainable>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Explode()
    {
        Destroy(gameObject);
    }
}
