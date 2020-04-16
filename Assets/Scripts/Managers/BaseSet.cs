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
    public enum Chars { END = -1, ONL, MASTER, CHARCOUNT };
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
        (new List<(Chars, Exprs, string)>{   //2번 대화 : 자기 책상 상호작용
            (Chars.ONL, Exprs.NORM, "내 책상에서 잠깐 쉬면서 회복을 할 수 있지."),
            (Chars.ONL, Exprs.CRY, "그런데 왜 침대가 아닌 거지..."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //3번 대화 : 회복
            (Chars.ONL, Exprs.SMILE, "회복했어!"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //4번 대화 : 사범 처음 만남
            (Chars.ONL, Exprs.SURPRISED, "어? 누구세요?"),
            (Chars.MASTER, Exprs.SMILE, "커맨드 기술을 배우고 싶은 건가? 잘 찾아왔네!"),
            (Chars.ONL, Exprs.SURPRISED, "아니, 누구신데 제 방에 계시냐고요!"),
            (Chars.MASTER, Exprs.NORM, "자네는 재능이 있어 보이는군... 하지만 배우고 싶다면 돈을 내야 한다네."),
            (Chars.ONL, Exprs.ANGRY, "말이 안 통하는데.."),
            (Chars.ONL, Exprs.CRY, "아, 참고로 여기까지야. 기본 기능들만 딱 모아 놓은 정도라고. 이 테스트가 끝나면 분량 늘리는 것만 남았으니까, 좋은 리포트 기대할게."),
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
            (Chars.ONL,Exprs.NORM, "무서우니까 일단 나가지 말고 들어가자.. 어차피 아직은 나가는 문도 없지만."),
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
    };

    public enum Flags {             //이벤트 플래그에 쉽게 접근시키기 위한 열거형. 이건 절대 순서 바꾸지 말자. 번호도 주석에 적어 두겠다.
        MYDESK=0,                     //0번. 책상에 말을 처음 걸었는가?  0: 말을 건 적 없음 / 1: 말을 건 적 있음(완)
        OUTEXP,                     //1번. 방 밖으로 나가 보았는가?    0: 아니 / 1: 나갔는데 드론을 안 잡음 / 2: 나가서 드론을 잡음 / 3: 나가서 드론 잡고 들어감(완)
        FLAGCOUNT                   //플래그 수. 이건 플래그가 아니다.
    };

    public enum Items            //아마 플래그와 같은 식으로 다뤄질 듯함. 아이템은 정렬이 필요한데 세이브에 문제가 없게 저장하는 방법 모색 필요
    {
        MONEY=0,
        ITEMCOUNT
    }

    public static Dictionary<string, string> Maps = new Dictionary<string, string>{
        { "MyRoom","강오늘의 방" },
        { "Corridor","복도" },
    };
}
