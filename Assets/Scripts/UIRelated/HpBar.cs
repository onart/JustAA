﻿using UnityEngine;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    public Player p;
    Image bar;
    Text txt;

    void Start()
    {
        foreach(var b in GetComponentsInChildren<Image>())
        {
            if (b.name == "HP") { 
                bar = b;
                break;
            }
        }
        txt = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.transform.localScale = new Vector2((float)p.HP / p.MHP, 1);
        txt.text = "HP " + p.HP + "/" + p.MHP;
    }
}
