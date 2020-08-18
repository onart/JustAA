﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//맵 입장 시 '일회성'으로 나오는 대사.
public class Six1Ev : MapEv
{
    public BaseSet.Flags flag;      //건드릴 플래그
    public int flag_to, dial_no;    //플래그 기준 수, 나오는 대사

    public override void Stt()
    {
        if (DataFiller.load_complete && p.FLAGS[(int)flag] < flag_to) 
        {
            StartCoroutine(D_Start(dial_no));
            p.FLAGS[(int)flag] = flag_to;
        }
    }
}