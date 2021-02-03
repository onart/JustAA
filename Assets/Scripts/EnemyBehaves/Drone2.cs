public class Drone2 : Enemy
{
    //점프를 할 수 있음, 넉백이 강력함, 다운은 있지만 공격력은 1 고정. 즉 지형과 다른 적과의 협공으로 짜증나게 하는 것이 목표
    // Update is called once per frame
    void Update()
    {

    }

    protected override void Move()
    {

    }

    protected override void St()
    {
        exp = 40;
        maxHp = (int)(40 * (SysManager.difficulty / 2.0f));
        hp = maxHp;
        at.face = 1;
        actTime = 0.5f / SysManager.difficulty;
        rage = 1000;
    }
}
