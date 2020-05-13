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
    public static string[] names = { "오늘", "사범", "노을" };
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
            (Chars.ONL,Exprs.NORM, "무서우니까 일단 나가지 말고 들어가자.. 무슨 일이 있는 것 같으니 1층에 CCTV 친구한테 연락해 보자고."),
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
            (Chars.MASTER, Exprs.NORM, "....그건 배우기도 어렵지만 쓰기도 까다로운 기술일세. 2000xp가 필요하지. 배우면 이름이 기니까 DNEDA로 줄여서 메뉴에 들어갈 걸세. "),
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
            (Chars.NOL, Exprs.SMILE, "이제야 연락하는구나. 별로 안 다쳤니?"),
            (Chars.ONL, Exprs.NORM, "'이야기를 참 빠르게도 진행하네'"),
            (Chars.ONL, Exprs.NORM, "CC야, 넌 잘 알고 있나 보네. 이게 무슨 일이야?"),
            (Chars.NOL, Exprs.SMILE, "난 CC가 아니라 노을이야."),
            (Chars.ONL, Exprs.ANGRY, "CC야,\n넌 잘 알고 있나 보네.\n이게 무슨 일이야?"),
            (Chars.NOL, Exprs.ANGRY, "거 참...\n어떤 이상한 사람이 지하로 들어가더니 한 30분 전부터 로봇이 막 올라가더라. 덕분에 엘리베이터도 고장나고 건물 상태가 말이 아니야."),
            (Chars.ONL, Exprs.SURPRISED, "조치는 취했어? 언제쯤 밖으로 나가도 될까?"),
            (Chars.NOL, Exprs.NORM, "무슨 말이야, 당연히 네가 해결해야지."),
            (Chars.ONL, Exprs.SURPRISED, "왜?"),
            (Chars.NOL, Exprs.NORM, "그걸 왜 몰라?             \n...지금은 모른대도 결국 자연히 알게 될 거야. 아무튼 해결을 위해 줄 게 있으니 일단 내 방으로 와 줘."),
            (Chars.ONL, Exprs.ANGRY, "후..."),
            (Chars.NOL, Exprs.CRY, "지금 나도 할 일이 많아. 그럼 오는 걸로 알고 있을게. 더 할 말 없지?"),
            (Chars.ONL, Exprs.NORM, "아, 잠깐. 한 가지 물어볼 게 더 있어. 방에 본 적 없는 사람이 들어왔는데 말이 안 통하거든. 네가 문 열어줬지?"),
            (Chars.NOL, Exprs.NORM, "어제랑 오늘은 외부인 출입이 없어.\n아 미안, 진짜 급하다. 이 이상은 만나면 얘기하자. 끊는다?"),
            (Chars.CHARCOUNT, Exprs.NORM,"통화가 종료되었습니다."),
            (Chars.ONL, Exprs.ANGRY, "이런, 일단 가 봐야겠다."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //18번 대화 : 맨 처음에 컴퓨터에 말을 걸었다
            (Chars.ONL, Exprs.NORM, "지금은 딱히 컴퓨터로 볼 일이 없다."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //19번 대화 : 그냥 맨~처음(맵이벤트)
            (Chars.ONL, Exprs.NORM, "배가 고픈데."),
            (Chars.ONL, Exprs.NORM, "집에 뭐가 없으니까 밖에 나가 보자."),
            (Chars.ONL, Exprs.NORM, "아, 참고로 조작은 Esc로 메뉴를 열면 확인할 수 있어."),
            (Chars.ONL, Exprs.SMILE, "Esc를 누르자! 누르자!"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //20번 대화 : 노을과의 통화를 마친 이후 컴퓨터
            (Chars.ONL, Exprs.NORM, "어서 101호로 가자."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //21번 대화 : 아직 아래층으로 내려가면 안 될 때
            (Chars.ONL, Exprs.NORM, "일단 집으로 돌아가는 게 좋을 것 같은데.."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //22번 대화 : 계단이 끊어짐을 발견
            (Chars.ONL, Exprs.SURPRISED, "엘리베이터만 망가진 게 아니라 계단도 못 쓰게 됐네?"),
            (Chars.ONL, Exprs.SURPRISED, "여기서 뛰어내릴 수는 없어. 11층으로 가서 다른 방법을 찾아야겠군."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //23번 대화 : 계단맵 첫 진입
            (Chars.ONL, Exprs.NORM, "여긴 적이 없네. 어서 움직이자."),
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
        new Options(5,"대시 공격(40xp)\n풀 스윙(1000xp)\n됐고 나가요, 이 대머리 아저씨야.\n체력을 강화한다\n필요 없다",3.5f),
    };

    public enum Flags {             //이벤트 플래그에 쉽게 접근시키기 위한 열거형. 이건 배포 후에는 절대 순서 바꾸지 말자. 번호도 주석에 적어 두겠다.
        MYBED=0,                    //0번. 침대에 말을 처음 걸었는가?  0: 말을 건 적 없음 / 1: 말을 건 적 있음(완)
        OUTEXP,                     //1번. 방 밖으로 나가 보았는가?    0: 아니 / 1: 나갔는데 드론을 안 잡음 / 2: 나가서 드론을 잡음 / 3: 나가서 드론 잡고 들어감 / 4: 관장과 첫 대화를 마침(완)
        SKILLS,                     //2번. 어떤 스킬을 배웠는가?       2^0자리: 대시공격, 2^1자리: 풀스윙 / 2^2자리: DNEDA, 추가될 수 있음
        STAGE1,                     //3번. 1스테이지와 관련된 플래그   0: 처음 / 1: 노을과 연락함 / 2: 12층에서 계단으로 내려가봄. / 3: 11층에서 매트리스를 얻음. / 4: 
        TUTORIAL,                   //4번. 튜토리얼인데 별 건 없다.    0: 처음. Esc를 눌러 조작설정을 보라고 안내하자. 1: 이미 봤다. 안내하지 말자.
        FLAGCOUNT                   //플래그 수. 이건 플래그가 아니다.
    };

    public static Dictionary<string, string> Maps = new Dictionary<string, string>{
        { "MyRoom","강오늘의 방" },
        { "Corridor","12층 복도" },
        { "Stairs","층계" },
    };
}
