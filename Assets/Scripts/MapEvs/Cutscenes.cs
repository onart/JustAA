using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscenes : MapEv
{
    Image im;

    void Start()
    {
        im = GetComponent<Image>();
        switch (cut)
        {
            case 0:
                im.sprite = Resources.Load<Sprite>("CutImages/LOGO");
                tm.Dialog_Start(38, this);
                break;
            default:
                break;
        }
    }

    public override void afterDialog()
    {
        switch (dialog)
        {
            case 38:
                var dr = FindObjectOfType<Door>();
                dr.connectedDoor = "Cut";
                dr.connectedScene = "Nowhere";
                dr.ObjAct();
                break;
            default:
                break;
        }
    }

    void backToMap()
    {

    }
}
