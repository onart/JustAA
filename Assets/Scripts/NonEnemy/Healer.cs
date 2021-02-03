public class Healer : HPChanger
{
    protected override void Act()
    {
        p.HpChange(delta);
        Destroy(gameObject);    //이 줄은 사라질 수도 있음.
    }
}
