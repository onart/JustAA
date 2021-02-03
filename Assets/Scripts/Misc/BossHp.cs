using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    int max, cur;
    public Image redBar;
    public TextMeshProUGUI txt;

    public void HpChange(int delta)
    {
        cur += delta;
        if (cur > max) cur = max;
        else if (cur < 0) cur = 0;
        redBar.transform.localScale = new Vector2((float)cur / max, 1);
        txt.text = cur + "/" + max;
    }

    public void SetMax(int max)
    {
        if (max == 0) Destroy(gameObject);
        this.max = max;
        cur = max;
        txt.text = max.ToString() + '/' + max.ToString();
    }
}
