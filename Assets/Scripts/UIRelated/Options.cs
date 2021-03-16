using System.Collections.Generic;
using System.Linq;

public class Options
{
    int number;         //선택지 수(세로로 선택상자를 늘임)
    List<int> exc;          //제외할 인덱스
    string body;
    float horizontal;   //(가로로 선택상자를 늘임)

    public Options(int num, string str, float hr)
    {
        number = num;
        body = str;
        horizontal = hr;
        exc = new List<int>();
    }

    public void delOps(List<int> idx)
    {
        exc = new List<int>(idx);
    }

    public int down1(int cur)   //무한루프 주의: exc가 전체집합과 동일한 경우
    {
        if (Number == 0) return 0;
        while (exc.Contains(++cur)) ;
        if (cur >= number)
        {
            return down1(-1);
        }
        else
        {
            return cur;
        }
    }

    public int up1(int cur)   //무한루프 주의: exc가 전체집합과 동일한 경우
    {
        if (Number == 0) return 0;
        while (exc.Contains(--cur)) ;
        if (cur < 0)
        {
            return up1(number);
        }
        else
        {
            return cur;
        }
    }

    public Options Clone()
    {
        return (Options)MemberwiseClone();
    }

    public int Number
    {
        get
        {
            return number - exc.Count();
        }
    }
    public string Body
    {
        get
        {
            string ret = "";
            var b = body.Split('\n');
            for (int i = 0; i < number; i++)
            {
                if (!exc.Contains(i))
                {
                    if (ret.Length == 0) ret += b[i];
                    else ret += ('\n' + b[i]);
                }
            }
            return ret;
        }
    }
    public float Hor
    {
        get { return horizontal; }
    }
}
