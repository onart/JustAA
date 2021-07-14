//맵 입장 시 '일회성'으로 나오는 대사.
public class Six1Ev : MapEv
{
    public BaseSet.Flags flag;      //건드릴 플래그
    public int flag_to, dial_no;    //플래그 기준 수, 나오는 대사

    protected override void Stt()
    {
        Re();
    }

    private void Re()
    {
        if (!DataFiller.load_complete)
        {
            Invoke(nameof(Re), 0.1f);
        }
        else if (p.FLAGS[(int)flag] < flag_to)
        {
            StartCoroutine(D_Start(dial_no));
        }
    }

    public override void afterDialog()
    {
        p.FLAGS[(int)flag] = flag_to;
    }
}
