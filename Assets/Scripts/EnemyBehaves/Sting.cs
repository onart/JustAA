public class Sting : Enemy
{
    //날아다니는 적, 하드 : 지형을 무시함
    void Update()
    {

    }

    protected override void St()
    {
        base.St();
        exp = 40;
        maxHp = (int)(15 * (SysManager.difficulty / 2.0f));
        hp = maxHp;
        at.face = 1;
        actTime = 0.5f / SysManager.difficulty;
        rage = 1000;
    }

    protected override void Move()
    {
        //평소의 움직임/포착시의 움직임/쿨타임(1패턴)
    }
}
