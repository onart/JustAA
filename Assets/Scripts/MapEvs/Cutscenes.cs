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
            default:
                setCut();
                break;
        }
        switch (cut)    //음향 효과 등
        {
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
                dr.connectedScene = "Container";
                dr.ObjAct();
                break;
            default:
                break;
        }
    }

    void backToMap()
    {

    }

    void setCut()
    {
        im.sprite = Resources.Load<Sprite>("CutImages/" + BaseSet.cutImages[cut]);
        int di = BaseSet.cutTexts[cut];
        if (di < 0) return;
        tm.Dialog_Start(BaseSet.cutTexts[cut], this);
    }
}
