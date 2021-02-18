using System.Collections.Generic;

//대화창 관련 : 캐릭터 리스트, 표정 리스트
//대화 리스트
//
public static class BaseSet
{
    /*Chars: 대화상자에서 캐릭터를 지칭하는 데에 쓰임
     END는 대화의 끝을 나타낼 때 쓰이며
     CHARCOUNT는 캐릭터 수를 나타내므로 오른쪽 끝에 고정할 것
    */
    public enum Chars { END = -1, ONL, MASTER, NOL, EUN, CHARCOUNT, J1 };
    public static string[] names = { "오늘", "사범", "노을", "은이", "", "J1" };  //빈 문자열 기준으로 왼쪽은 얼굴 있음, 오른쪽은 얼굴 없음
    /*Exprs: 대화상자에서 표정을 지칭하는 데에 쓰임
     FACECOUNT는 캐릭터 수를 나타내므로 오른쪽 끝에 고정할 것
    */
    public enum Exprs { NORM = 0, SMILE, CRY, ANGRY, SURPRISED, FACECOUNT };

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
            (Chars.ONL, Exprs.SURPRISED, "복도에 왜 저런 게 있을까?"),
            (Chars.ONL,Exprs.NORM, "위험할 것 같으니 일단 나가지 말고 들어가자.. 무슨 일이 있는 것 같으니 관리실에 연락해 봐야지."),
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
            (Chars.MASTER, Exprs.NORM, "....그건 배우기도 어렵지만 쓰기도 까다로운 기술일세. 2000xp가 필요하지. 배우면 이름이 기니까 '공중 대시'로 줄여서 메뉴에 들어갈 걸세. "),
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
            (Chars.ONL, Exprs.NORM, "관리자야, 넌 잘 알고 있나 보네. 이게 무슨 일이야?"),
            (Chars.NOL, Exprs.SMILE, "내 이름은 관리자가 아니라 노을이야."),
            (Chars.ONL, Exprs.ANGRY, "관리자야,\n넌 잘 알고 있나 보네.\n이게 무슨 일이야?"),
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
            (Chars.ONL, Exprs.NORM, "밥 없으니까 밖에 나가 보자."),
            (Chars.ONL, Exprs.NORM, "아, 조작법은 Esc로 메뉴를 열어서 확인할 수 있어."),
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
            (Chars.ONL, Exprs.SURPRISED, "엘리베이터만 망가진 게 아니라 계단도 못 쓰게 됐잖아?"),
            (Chars.ONL, Exprs.SURPRISED, "여기서 뛰어내릴 수는 없어. 11층으로 가서 다른 방법을 찾아야겠군."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //23번 대화 : 계단맵 첫 진입
            (Chars.ONL, Exprs.NORM, "굳이 다른 데를 들를 시간은 없겠지?"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //24번 대화 : 체력을 강화한다
            (Chars.MASTER, Exprs.NORM, "상황이 많이 어려운 모양이군. 500XP로 체력 10을 올릴 수 있네."),
            (Chars.MASTER, Exprs.NORM, "기술적으로 한꺼번에 여러 번 강화하게 하는 건 쉽지만, 굳이 지원하지는 않겠네. 적은 체력으로 클리어를 노려보게나."),
            (Chars.MASTER, Exprs.NORM, "체력을 강화하겠는가?"),
            (Chars.END, Exprs.NORM, "")
        },1),
        (new List<(Chars, Exprs, string)>{   //25번 대화 : 체력 강화 경험치 모자람
            (Chars.MASTER, Exprs.NORM, "XP가 부족하군. 조금만 진행하다 보면 기술을 다 배우고도 남아돌 걸세."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //26번 대화 : 점프대 설명문
            (Chars.CHARCOUNT, Exprs.NORM, "이것에 닿으면 주먹이 바라보는 방향으로 날아갑니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "힘은 주먹마다 다르며, 주인공 캐릭터뿐 아니라 모든 움직이는 물체가 영향을 받습니다."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //27번 대화 : 6-1구역 첫 입장
            (Chars.ONL, Exprs.NORM, "여긴 처음 와 보네."),
            (Chars.ONL, Exprs.NORM, "보통 상가 시설은 아닌가? 생각보다 깊게 펼쳐져 있네."),
            (Chars.ONL, Exprs.NORM, "내려가는 곳은 아니지만 당장에 갈 수 있는 곳이 저기뿐이니 갈 수밖에 없군."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //28번 대화 : 6-2구역 첫 입장
            (Chars.ONL, Exprs.NORM, "점점 깊어지는데, 온 길로 돌아갈 수 없는 구조로군."),
            (Chars.ONL, Exprs.ANGRY, "후.. 그냥 집에 가고 싶은데 이걸 내가 왜 하나.."),
            (Chars.ONL, Exprs.SMILE, "최소한 안심하고 저장할 수는 있겠다!"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //29번 대화 : 6-2구역 가시 설명문
            (Chars.CHARCOUNT, Exprs.NORM, "가시에 닿으면 난이도에 관계 없이 게임 오버입니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "검정색 움직이는 발판은 벽에 닿을 때까지 나아갑니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "미끄러질 수 있으니 균형은 직접 잡으셔야 합니다."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //30번 대화 : 6-3구역 대시 설명문
            (Chars.CHARCOUNT, Exprs.NORM, "이 앞의 가시는 대시로 지나갈 수 있습니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "일부 공격을 통과하는 데 쓸 수도 있으니 익숙해져 보아요."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //31번 대화 : 6-4구역 레이저 설명문
            (Chars.CHARCOUNT, Exprs.NORM, "레이저에 닿으면 빠르게 체력이 깎입니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "적들은 레이저에 피해를 입지 않지만 레이저를 가릴 수 있습니다."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //32번 대화 : 도움말
            (Chars.CHARCOUNT, Exprs.NORM, "이것은 도움말입니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "이렇게 생긴 것을 보면 조사해 보세요."),
            (Chars.CHARCOUNT, Exprs.NORM, "상호작용은 정지 상태에서만 가능합니다."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //33번 대화 : 6-5구역 입장 대사
            (Chars.ONL, Exprs.SURPRISED, "이쪽으로 와도 결국 믿음의 도약이라니.."),
            (Chars.ONL, Exprs.NORM, "라고 생각했는데 생각보다 높진 않네."),
            (Chars.ONL, Exprs.NORM, "가시가 많으니 조심하자."),
            (Chars.ONL, Exprs.NORM, "이 앞에 튀어나온 벽 쪽이 왠지 안전할 것 같은데?"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //34번 대화 : 보스 안내판 1
            (Chars.CHARCOUNT, Exprs.NORM, "이것은 보스 도움말입니다.\n보스를 언제 때리는 게 좋을지 모르겠을 때 보면 좋아요."),
            (Chars.CHARCOUNT, Exprs.NORM, "지금은 그냥 나왔지만 나중에 가면 이것 또한 닿기 어려운 곳에 위치할 겁니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "다음은 첫 중간보스 설명입니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "이 보스는 하이퍼아머인 데다가 특별히 공격 타이밍을 주지 않습니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "조금이라도 허점이 보일 때 치고 빠집시다."),
            (Chars.CHARCOUNT, Exprs.NORM, "단, 멀어지면 다른 패턴을 사용하니 가까이 있는 게 좋습니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "사범에게서 대시 공격을 배웠다면 조금 편할 겁니다."),
            (Chars.CHARCOUNT, Exprs.NORM, "어려움 모드를 플레이하고 계신다면, 초장에 던지는 나이프는 사라지지 않으니 주의하세요."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //35번 대화 : 마지막방 중간보스 전 대사
            (Chars.ONL, Exprs.ANGRY, "아니, 문이 왜 저 위에 달려 있지?"),
            (Chars.ONL, Exprs.ANGRY, "그나저나...."),
            (Chars.ONL, Exprs.NORM, "건물에서 약간 튀어나온 부분이 여기구나. 언제 꺾어질지 불안해 보이는 구조였는데 여길 와 보네. 경치는 참 좋다."),
            (Chars.ONL, Exprs.NORM, "그나저나 내려가는 데 도움이 되는 물건이.."),
            (Chars.J1, Exprs.NORM, "안녕하세요? 마사지 서비스를 받으러 오신 분인가요?"),
            (Chars.ONL, Exprs.SURPRISED, "...??!! 손에 저런 걸 들고 마사지를 한다고?"),
            (Chars.J1, Exprs.NORM, "그럼 바로 시작하겠습니다!"),
            (Chars.ONL, Exprs.ANGRY, "들어온다! 정신 똑바로 차리자!"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //36번 대화 : 중간보스 클리어 후 대사
            (Chars.J1, Exprs.NORM, "수고하셨어요. 이제 1층으로 내려보내 드릴게요."),
            (Chars.ONL, Exprs.ANGRY, "뭐 이 자식아?"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //37번 대화 : 중간보스 클리어 후 대사 2
            (Chars.ONL, Exprs.CRY, "......"),
            (Chars.ONL, Exprs.CRY, "집에서 잠이나 잘걸..."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //38번 대화 : 중간보스 클리어 후 컷씬
            (Chars.ONL, Exprs.SURPRISED, "어어!! 기울어진다!!"),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //39번 대화 : 임시
            (Chars.CHARCOUNT, Exprs.NORM, "이 맵에서는 나갈 수 없습니다."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //40번 대화 : 테마1 시작
            (Chars.ONL, Exprs.CRY, "으..."),
            (Chars.EUN, Exprs.SMILE, "일어났구나!"),
            (Chars.ONL, Exprs.CRY, "여기가.. 어디죠?"),
            (Chars.EUN, Exprs.SMILE, "내 집이야. 저어기 상가 건물 위쪽이 터져서 여기까지 날아왔길래 조사해 보니 네가 있더라고."),
            (Chars.ONL, Exprs.NORM, "후.. 아무튼 감사합니다."),
            (Chars.EUN, Exprs.NORM, "그쪽에 살고 있었다면 당장 돌아갈 곳이 없을 텐데, 날 도와주기로 하면 여기를 마음대로 쓰게 해 줄게."),
            (Chars.ONL, Exprs.SMILE, "(오! 이렇게 잘 생긴 사람이랑 같이 사는 건가? 내 인생에도 드디어 봄날이..)"),
            (Chars.EUN, Exprs.NORM, "유감이지만 내 애가 둘이야. 기러기 아빠라서 딱히 다른 사람이 올 일은 없을 거지만."),
            (Chars.ONL, Exprs.ANGRY, "..멋대로 생각을 읽지 말아줄래요?"),
            (Chars.ONL, Exprs.NORM, "아무튼, 도움이 필요한 게 있다고요?"),
            (Chars.EUN, Exprs.NORM, "그래. 이 앞 바다동굴에 거대한 뱀이 살고 있는데, 날아온 건물 때문에 먹이 유입이 줄어든 모양이야."),
            (Chars.EUN, Exprs.NORM, "그래서 가끔씩 슬금슬금 기어나오고 있어. 지금 겨울이라 사람이 거의 없어서 다행인데 날이 풀리기 전에 대책이 필요하거든."),
            (Chars.ONL, Exprs.SURPRISED, "설마 그걸 퇴치하라는?"),
            (Chars.EUN, Exprs.NORM, "그건 안 돼. 국가에서 보호되고 있거든. 다행히 밖에 나와서 사람을 발견하는 일만 없으면 온순한 편이야."),
            (Chars.ONL, Exprs.SURPRISED, "그럼 밥이라도 주러 가나요?"),
            (Chars.EUN, Exprs.NORM, "결론부터 말하면.. 그게 떨어진 부분 근처가 얼어 있단다. 그걸 깨고 원인을 찾아 없애야 해."),
            (Chars.ONL, Exprs.NORM, "그리 어렵게 들리지는 않네요."),
            (Chars.EUN, Exprs.CRY, "그것이.. 얼음이 상당히 두꺼운데 깨는 동안에 뱀한테 발견되면 위험해. 뱀 주제에 겨울잠은 왜 안 자나.."),
            (Chars.ONL, Exprs.NORM, "음.."),
            (Chars.EUN, Exprs.NORM, "자세한 설명은 현장에서 해 줄게. 오른쪽으로 쭉 가면 바다동굴이 나오거든. 천천히 쉬다 와. 나는 가서 기다리고 있을게."),
            (Chars.EUN, Exprs.NORM, "어차피 여기는 절벽 아래라서 나가려면 동굴을 지나가야 해."),
            (Chars.ONL, Exprs.NORM, "알았어요. 조금만 쉬었다가 갈게요."),
            (Chars.EUN, Exprs.NORM, "그래, 이따 보자."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //41번 대화 : 컨테이너 밖에서
            (Chars.ONL, Exprs.NORM, "가만 생각해 보면 이 일을 꾸민 범인은 관리실 그놈 같아.. 가만 안 두겠어."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //42번 대화 : 동굴 첫 입장
            (Chars.EUN, Exprs.NORM, "왔구나. 준비는 된 거겠지?"),
            (Chars.ONL, Exprs.NORM, "네."),
            (Chars.EUN, Exprs.NORM, "이 동굴은 꽤 크지만 외길이야. 오른쪽으로 쭉 가다 보면 막힌 곳이 있는데, 거기에 뱀이 있지."),
            (Chars.EUN, Exprs.NORM, "그리고 그쪽도 물가야. 얼어 있지만.. 그걸 깨면 문제가 해결될 거야."),
            (Chars.ONL, Exprs.NORM, "달리 조심할 건 뱀 말고는 없는 건가요?"),
            (Chars.EUN, Exprs.NORM, "아, 말이 나와서 기억났는데, 여기 사는 게들이 사람만큼 커."),
            (Chars.EUN, Exprs.NORM, "그리고 골짜기가 깊으니 발조심하고."),
            (Chars.ONL, Exprs.CRY, "네."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //43번 대화 : 동굴에서 은이한테 말 걸 때
            (Chars.ONL, Exprs.SMILE, "같이 가실래요?"),
            (Chars.EUN, Exprs.CRY, "가정이 있는 몸이라서.."),
            (Chars.EUN, Exprs.NORM, "하지만 난 수영을 잘 하니 골짜기로 떨어졌을 때만큼은 바로 가서 구해줄 수 있어."),
            (Chars.CHARCOUNT, Exprs.NORM, "이번 스테이지에서는 낙하 피해가 반으로 감소합니다."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //44번 대화 : 동굴 도움말
            (Chars.CHARCOUNT, Exprs.NORM, "다음 맵부터는 일정 주기로 진동이 있을 겁니다. 그 때 지면에 붙어 있다면 중심을 잃으니 조심하세요."),
            (Chars.END, Exprs.NORM, "")
        },0),
        (new List<(Chars, Exprs, string)>{   //45번 대화 : 동굴 도움말2
            (Chars.CHARCOUNT, Exprs.NORM, "보통 이상 난이도에서는 게를 일정 수 이상 퇴치해야 지나갈 수 있습니다."),
            (Chars.END, Exprs.NORM, "")
        },0),
    };

    //선택지 리스트. 필요시 텍스트 크기도 지정해 주자..
    public static Options[] ops =
    {
        new Options(0,"",0),        //선택지 없음을 나타냄
        new Options(2,"예\n아니요",1.1f),
        new Options(6,"대시 공격(40xp)\n박치기(1000xp)\n붕붕이(2000xp)\n됐고 나가요, 이 대머리 아저씨야.\n체력을 강화한다(500xp)\n필요 없다",3.5f),
    };

    public enum Flags
    {             //이벤트 플래그에 쉽게 접근시키기 위한 열거형. 이건 배포 후에는 절대 순서 바꾸지 말자. 번호도 주석에 적어 두겠다.
        MYBED = 0,                    //0번. 침대에 말을 처음 걸었는가?  0: 말을 건 적 없음 / 1: 말을 건 적 있음(완)
        OUTEXP,                     //1번. 방 밖으로 나가 보았는가?    0: 아니 / 1: 나갔는데 드론을 안 잡음 / 2: 나가서 드론을 잡음 / 3: 나가서 드론 잡고 들어감 / 4: 관장과 첫 대화를 마침(완)
        SKILLS,                     //2번. 어떤 스킬을 배웠는가?       2^0자리: 대시공격, 2^1자리: 박치기 / 2^2자리: 공중대시 / 2^3자리: 공중회전공격, 추가 가능
        STAGE1,                     //3번. 0.5테마와 관련된 플래그     0: 처음 / 1: 노을과 연락함 / 2: 12층에서 계단으로 내려가봄. / 3: 6-1구역 입장해봄 / 4: 6-2구역 입장해봄 / 5: 6-5구역 입장해봄 / 6: 0.5스테이지 클리어
        TUTORIAL,                   //4번. 튜토리얼인데 별 건 없다.    0: 처음. Esc를 눌러 조작설정을 보라고 안내하자. 1: 이미 봤다. 안내하지 말자.
        KEYS,                       //5번. 아이템을 말한다.            2^0자리: 
        STAGE2,                     //6번. 1테마와 관련된 플래그       0: 처음 / 1: 첫 대화를 종료함 / 2: 집 밖으로 나가 봄 / 
        STAGE3,                     //7번. 2테마와 관련된 플래그       0: 처음
        STAGE4,                     //8번. 3테마와 관련된 플래그       0: 처음
        STAGE5,                     //9번. 4테마와 관련된 플래그       0: 처음
        FLAGCOUNT                   //플래그 수. 이건 플래그가 아니다.
    };

    public static Dictionary<string, string> Maps = new Dictionary<string, string>{
        { "MyRoom","강오늘의 방" },
        { "12Corridor","12층 복도" },
        { "Stairs","층계" },
        { "11Corridor","11층 복도" },
        { "Six_1","11층 방" },
        { "Six1","11층-1구역" },
        { "Six2","11층-2구역" },
        { "Six3","11층-3구역" },
        { "Six4","11층-4구역" },
        { "Six5","11층-5구역" },
        { "Six6","11층-끝방" },
        { "Beach","해변" },
        { "CaveFront","바다동굴 앞" },
        { "Container","바다동굴 근처 컨테이너" },
        { "Cave1","<color=white>바다동굴1</color>" },
        { "Cave2","<color=white>바다동굴2</color>" },
        { "Cave3","<color=white>바다동굴3</color>" },
        { "Cave4","<color=white>바다동굴4</color>" },
        { "CutScene","" },
        { "Nowhere","테스트 방" },
    };

    public static string[] cutImages ={ //컷씬 이미지 이름
        "Building1",     //0번: 11층 터짐
    };

    public static int[] cutTexts = { //컷씬 이미지 대응 대사 번호
        38,
    };
}
