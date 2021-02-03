using TMPro;
using UnityEngine;

public class DmgOrHeal : MonoBehaviour
{
    public TextMeshPro dmgTxt;
    float alpha;

    private void Start()
    {
        alpha = 1.2f;
        dmgTxt.sortingLayerID = SortingLayer.NameToID("FX");
    }

    void Update()
    {
        alpha -= 0.02f;
        if (alpha > 0)
        {
            dmgTxt.color = new Color(1, 1, 1, alpha);
            transform.position = new Vector2(transform.position.x, transform.position.y + alpha / 40);
        }
        else Destroy(gameObject);
    }

    public void SetText(string delta, Color c, int size = 5)
    {
        dmgTxt.text = delta;
        dmgTxt.faceColor = c;
        dmgTxt.fontSize = size;
    }
}
