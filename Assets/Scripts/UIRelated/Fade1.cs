using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade1 : MonoBehaviour
{
    Image im;
    float br;                         //밝기 지수
    public GameObject title;
    // Start is called before the first frame update
    void Start()
    {
        br = 0;
        im = GetComponent<Image>();
        im.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        br += 0.02f;
        if (br <= 1) im.color = new Color(br, br, br);
        else if (br <= 2) im.color = new Color(2 - br, 2 - br, 2 - br);
        else {
            title.SetActive(true);
            Destroy(gameObject); 
        }
    }
}
