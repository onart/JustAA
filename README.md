# 다음 과제.

### 난이도 시스템(3단계 정도, 잡몹은 반응속도 차이(체력차이 없음), 보스는 체력과 패턴 차이 정도로)
>Sysmanager.difficulty에 1,2,3
>잡몹 체력/속도 등 차이
>보스 체력/패턴 차이
>어려움에 일부 세이브포인트 없음
>장애물 몇 개 차이

### 코루틴은 지금처럼 invoke로 처리, enemy 지능은 부모 추상클래스에서 이미 코루틴으로 재정의해 두고 가겠음. 상태머신과 잘 연계해라.
>고정패턴은 (행동이름, 지연시간)의 배열로 처리할 수 있을듯

### 현재 문을 통한 이동으로 구현된 것을 일반화하자. 예를 들면 대화 후 맵 이동이나, 일정 위치에 가면 맵 이동 등..

->상하 방향공격 추가(공중 상단 공격, 지상 올려치기, 지상 내려치기). 아마 공격력은 공중 1, 지상 2 정도가 될 

->기술마다 다른 소리가 나도록

->일반 효과음 및 BGM 직접 

### 추가 예정: fps 30, 60 중 하나 선택


![image](https://github.com/onart/JustAA/blob/master/%EA%B3%BC%EC%A0%951.png?raw=true)
