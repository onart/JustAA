public class Options
{
    int number;         //선택지 수(세로로 선택상자를 늘임)
    string body;
    float horizontal;   //가로로 선택상자를 늘임)

    public Options(int num, string str, float hr)
    {
        number = num;
        body = str;
        horizontal = hr;
    }

    public int Number{
        get { return number; }
    }
    public string Body
    {
        get { return body; }
    }
    public float Hor
    {
        get { return horizontal; }
    }
}
