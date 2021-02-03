using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    public Player p;
    Image bar;
    TextMeshProUGUI txt, exptxt;

    void Start()
    {
        foreach (var b in GetComponentsInChildren<Image>())
        {
            if (b.name == "HP")
            {
                bar = b;
                break;
            }
        }
        txt = GetComponentsInChildren<TextMeshProUGUI>()[0];
        exptxt = GetComponentsInChildren<TextMeshProUGUI>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        bar.transform.localScale = new Vector2((float)p.HP / p.MHP, 1);
        txt.text = "HP " + p.HP + "/" + p.MHP;
        exptxt.text = p.exp + " EXP";
    }
}
