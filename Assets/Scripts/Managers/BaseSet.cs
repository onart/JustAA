using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//대화창 관련 : 캐릭터 리스트, 표정 리스트
//대화 리스트
//
public static class BaseSet
{
    /*Chars: 대화상자에서 캐릭터를 지칭하는 데에 쓰임
     END는 대화의 끝을 나타낼 때 쓰이며
     CHARCOUNT는 캐릭터 수를 나타내므로 오른쪽 끝에 고정할 것
    */
    public enum Chars { END = -1, ONL, MASTER, NOL, CHARCOUNT };
    public static string[] names = { "오늘", "사범" };
    /*Exprs: 대화상자에서 표정을 지칭하는 데에 쓰임
     FACECOUNT는 캐릭터 수를 나타내므로 오른쪽 끝에 고정할 것
    */
    public enum Exprs { NORM=0, SMILE, CRY, ANGRY, SURPRISED, FACECOUNT };

    //대사 리스트. 여기에 필요한 걸 추가하도록 하자..
    public static (List<(Chars, Exprs, string)>, int)[] talkData = {    //Chars : 캐릭터, Exprs, 표정, string : 내용, int : 대화 끝에 선택지 번호(0번은 선택지 없음)

        (new List<(Chars, Exprs, string)>{   //0번 대화 : 저장용
            (Chars.ONL, Exprs.NORM, "저장할까?"),
            (Chars.END, Exprs.NORM, "")  //모든 대화의 끝에 붙어야 함.
        },1),
        (new List<(Chars, Exprs, string)>{   //1번 대화 : 저장용
            (Chars.CHARCOUNT, Exprs.NORM, "저장되었습니다."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //2번 대화 : 자기 침대 상호작용
            (Chars.ONL, Exprs.NORM, "침대에서 잠깐 쉬면서 회복을 할 수 있지."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //3번 대화 : 회복
            (Chars.ONL, Exprs.SMILE, "회복했어!"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //4번 대화 : 사범 처음 만남
            (Chars.ONL, Exprs.SURPRISED, "어? 누구세요?"),
            (Chars.MASTER, Exprs.SMILE, "싸움의 기술을 배우고 싶은 건가? 잘 찾아왔네!"),
            (Chars.ONL, Exprs.SURPRISED, "아니, 누구신데 제 방에 계시냐고요!"),
            (Chars.MASTER, Exprs.NORM, "자네는 재능이 있어 보이는군... 하지만 아직 경험이 부족한 것 같네."),
            (Chars.ONL, Exprs.ANGRY, "말이 안 통하는데.."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //5번 대화 : 방 밖으로 처음 나감
            (Chars.ONL, Exprs.NORM, "라면이나 하나 사 와야지."),
            (Chars.ONL, Exprs.SURPRISED, "어! 저건 뭐지?"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //6번 대화 : 괴물이 있는 상태에서 다시 들어가려 할 때
            (Chars.ONL, Exprs.ANGRY, "일단 저것부터 어떻게든 해 보자!"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //7번 대화 : 괴물을 처치한 직후
            (Chars.ONL, Exprs.SURPRISED, "복도에 왜 저런 게 있어?"),
            (Chars.ONL,Exprs.NORM, "무서우니까 일단 나가지 말고 들어가자.. 무슨 일이 있는 것 같으니 1층 친구한테 연락해 보자고."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //8번 대화 : 관장에게 기술을 배우기 전 랜덤 대사 1
            (Chars.MASTER, Exprs.SMILE, "혹시 나에게 무기를 휘두르고 있는 건 아니지? 어차피 그래도 아무 일도 없을 걸세."),
            (Chars.ONL, Exprs.SURPRISED, "...!"),
            (Chars.MASTER, Exprs.SMILE, "자, 배우겠는가?"),
            (Chars.END, Exprs.NORM, "")
        },2),
        (new List<(Chars, Exprs, string)>{   //9번 대화 : 관장에게 기술을 배우기 전 랜덤 대사 2
            (Chars.MASTER, Exprs.SMILE, "나는 수상한 사람이 아닐세. 참고로 자네가 이곳을 떠나도 자네가 가는 곳에 내가 있을 게야. 기술은 배워야지."),
            (Chars.ONL, Exprs.NORM, "...."),
            (Chars.MASTER, Exprs.SMILE, "자, 배우겠는가?"),
            (Chars.END, Exprs.NORM, "")
        },2),
        (new List<(Chars, Exprs, string)>{   //10번 대화 : 관장에게 기술을 배우기 전 랜덤 대사 3
            (Chars.MASTER, Exprs.NORM, "...."),
            (Chars.ONL, Exprs.NORM, "????"),
            (Chars.MASTER, Exprs.SMILE, "자, 배우겠는가?"),
            (Chars.END, Exprs.NORM, "")
        },2),
        (new List<(Chars, Exprs, string)>{   //11번 대화 : 정말로 배울거냐
            (Chars.MASTER, Exprs.NORM, "정말로 배울 건가?"),
            (Chars.END, Exprs.NORM, "")
        },1),
        (new List<(Chars, Exprs, string)>{   //12번 대화 : 경험 부족
            (Chars.MASTER, Exprs.NORM, "아직은 소화하기 어려운 기술이네. 경험을 더 쌓도록."),
            (Chars.ONL, Exprs.CRY, "귀찮아..."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //13번 대화 : 배운 기술
            (Chars.MASTER, Exprs.NORM, "이미 배운 기술이군. 메뉴를 열면 사용 방법을 확인할 수 있네."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //14번 대화 : 기술 배우기 성공
            (Chars.MASTER, Exprs.NORM, "대단하군. 기술을 배웠으니 메뉴를 열면 사용 방법을 확인할 수 있네."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //15번 대화 : 나가 대머리야
            (Chars.MASTER, Exprs.CRY, "........................................자네"),
            (Chars.ONL, Exprs.SURPRISED, "'내가 너무 심했나.. 그래도 처음으로 말을 알아들은 것 같아서 기분이 묘하네.'"),
            (Chars.MASTER, Exprs.NORM, "....그건 배우기도 어렵지만 쓰기도 까다로운 기술일세. 2000 경험치가 필요하지."),
            (Chars.ONL, Exprs.ANGRY, "기술은 무슨! 내 방에서 나가세요!!!"),
            (Chars.MASTER, Exprs.NORM, "정말로 배울 건가?"),
            (Chars.ONL, Exprs.CRY, "'속 터져'"),
            (Chars.END, Exprs.NORM, "")
        },1),
        (new List<(Chars, Exprs, string)>{   //16번 대화 : 관장을 안 보고 컴퓨터에 말을 건 경우
            (Chars.ONL, Exprs.ANGRY, "잠깐만. 머리 위가 느낌이 이상한데.. 위쪽부터 확인해 보자."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //17번 대화 : 컴퓨터로 친구와 대화하며 문제를 파악
            (Chars.CHARCOUNT, Exprs.NORM, "101호 인터폰으로 연결합니다."),
            (Chars.CHARCOUNT, Exprs.NORM,".........\n..........\n...........\n............"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //18번 대화 : 맨 처음에 컴퓨터에 말을 걸었다
            (Chars.CHARCOUNT, Exprs.NORM, "안녕하세요! Esc로 메뉴를 열어 조작법을 확인해 보아요! 메뉴 안의 버튼을 클릭해서 조작설정을 바꿀 수 있습니다!"),
            (Chars.END, Exprs.NORM, "")
        },0),
    };

    //화면 중앙에 띄우는 글씨 리스트.
    public static string[] Notes =
    {
        //0번대
        "중앙 알림",    //형식용
    };

    //선택지 리스트. 필요시 텍스트 크기도 지정해 주자..
    public static Options[] ops =
    {
        new Options(0,"",0),        //선택지 없음을 나타냄
        new Options(2,"예\n아니요",1.1f),
        new Options(5,"대시 공격(40)\n풀 스윙(1000)\n됐고 나가요, 이 대머리 아저씨야.\n체력을 강화한다\n필요 없다",3.5f),
    };

    public enum Flags {             //이벤트 플래그에 쉽게 접근시키기 위한 열거형. 이건 절대 순서 바꾸지 말자. 번호도 주석에 적어 두겠다.
        MYBED=0,                    //0번. 침대에 말을 처음 걸었는가?  0: 말을 건 적 없음 / 1: 말을 건 적 있음(완)
        OUTEXP,                     //1번. 방 밖으로 나가 보았는가?    0: 아니 / 1: 나갔는데 드론을 안 잡음 / 2: 나가서 드론을 잡음 / 3: 나가서 드론 잡고 들어감 / 4: 관장과 첫 대화를 마침(완)
        SKILLS,                     //2번. 어떤 스킬을 배웠는가?       2^0자리: 대시공격, 2^1자리: 풀스윙 / 2^2자리: DNEDA, 추가될 수 있음
        STAGE1,                     //3번. 1스테이지와 관련된 플래그   0: 처음 / 1: 
        FLAGCOUNT                   //플래그 수. 이건 플래그가 아니다.
    };

    public static Dictionary<string, string> Maps = new Dictionary<string, string>{
        { "MyRoom","강오늘의 방" },
        { "Corridor","12층 복도" },
    };
}
