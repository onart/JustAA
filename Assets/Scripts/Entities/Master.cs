using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : Entity
{
    int learnskill;     //선택된 기술

    protected override void OnRecieve()
    {
        switch (dialog)
        {
            case 8:
            case 9:
            case 10:    //8,9,10번은 배울 기술의 선택
                if (selection < 2)
                {
                    if (LearnCheck(selection)) Invoke("AlreadyLearned", 0.02f);
                    else
                    {
                        learnskill = selection;
                        Invoke("LearnGeneral", 0.02f);
                    }
                }
                return;
            case 11:
                if (selection == 0)
                {
                    switch (learnskill)
                    {
                        case 0:
                            CostCheck(0, 40);
                            return;
                        case 1:
                            CostCheck(1, 1000);
                            return;
                        case 2:
                            CostCheck(2, 2000);
                            return;
                    }
                }
                return;
        }
    }

    //여기부터 invoke 대화ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    void LearnGeneral() 
    {
        D_Start(11);
    }
    void YouCant()
    {
        D_Start(12);
    }
    void AlreadyLearned()
    {
        D_Start(13);
    }
    void LearnSucc()
    {
        D_Start(14);
    }
    //여기까지 Invoke 대화ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    bool LearnCheck(int skill)   //이미 배운 기술인지 체크
    {
        return (p.FLAGS[(int)BaseSet.Flags.SKILLS] >> skill) % 2 == 1;
    }

    void CostCheck(int skill, int cost)    //충분한 경험치 있는지 체크
    {
        if (p.exp < cost)
        {
            Invoke("YouCant", 0.02f);
        }
        else
        {
            p.exp -= cost;
            p.FLAGS[(int)BaseSet.Flags.SKILLS] += (1 << skill);
            Invoke("LearnSucc", 0.02f);
        }
    }

    public override void ObjAct()
    {
        if (p.transform.position.x < transform.position.x) { 
            transform.localScale = new Vector2(-0.3f, 0.3f);
            p.transform.localScale = new Vector2(0.3f, 0.3f);
        }
        else { 
            transform.localScale = new Vector2(0.3f, 0.3f);
            p.transform.localScale = new Vector2(-0.3f, 0.3f);
        }
        if (p.FLAGS[(int)BaseSet.Flags.OUTEXP] == 4) {
            D_Start(Random.Range(8, 11));
        }
        else if(p.FLAGS[(int)BaseSet.Flags.OUTEXP] == 3)
        {
            D_Start(4);
            p.FLAGS[(int)BaseSet.Flags.OUTEXP] = 4;
        }
    }

    public override void St()
    {
        spacepos = new Vector3(0, 1.3f, 0);
        rayorigin = new Vector3(-0.6f, 0.5f, 0);
        raydir = Vector2.right;
        raydistance = 1.2f;
        Invoke("Disappear", 0.05f);
    }

    public override void Up()
    {       
    }

    void Disappear()    //이벤트를 갖추지 않으면 관장이 나타나지 않음
    {
        if (p.FLAGS[(int)BaseSet.Flags.OUTEXP] < 3) { gameObject.SetActive(false); }
    }
}
