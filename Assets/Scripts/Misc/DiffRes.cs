using UnityEngine;

//특정 난이도 이상/이하에만 존재하는 사물
public class DiffRes : MonoBehaviour
{
    public enum UnD { 이상, 이하 };
    public enum Dff { 쉬움 = 1, 보통 = 2, 어려움 = 3 };
    public Dff diff;    //diff : 기준 난이도. 1~3,  
    public UnD un;      //이상에만 존재(0)/이하에만 존재(1)

    void Start()
    {
        Re();
    }

    private void Re()
    {
        if (!DataFiller.load_complete)
        {
            Invoke("Re", 0.1f);
        }
        else
        {
            if (un == UnD.이상)
            {
                if (SysManager.difficulty < (int)diff) Destroy(gameObject);
            }
            else
            {
                if (SysManager.difficulty > (int)diff) Destroy(gameObject);
            }
        }
    }

}
