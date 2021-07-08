using System.Collections;
using UnityEngine;

/*
 보스 스크립트 특성
1. 보스는 상시 적대적
2. 애니메이션 패러미터를 사용할 것
3. 애니메이션 이벤트를 활용할 것: 인수는 0~1개만 가능
 */
public abstract class Boss : BaseHzd  //보스는 상시 적대적이므로 상태머신 전이기 없음. 고정적으로 패턴을 반복하는 애니메이션형과, 일반 적과 같은 형식이나 더 화려한 형태로 나뉨
{
    public BossHp bossBar;              //보스 전용 HP바
    protected float actTime;

    protected override void St()
    {
        bossBar.SetMax(maxHp);
    }

    public override void GetHit(int delta, Vector2 force)
    {
        HPChange(-delta);
    }

    protected override IEnumerator OnZero()
    {
        //GetComponent<Collider2D>().enabled = false;
        p.gameObject.GetComponent<Player>().GainExp(exp);
        if (at) at.enabled = false;
        CancelInvoke();
        float alpha = 1;
        Destroy(at);
        while (alpha > 0)
        {
            alpha -= 0.02f;
            sr.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }

    protected override void HPChange(int delta)
    {
        base.HPChange(delta);
        bossBar.HpChange(delta);
    }
}
