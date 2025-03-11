## [팀스파르타] 2D 모바일 게임 과제

### TDS_Tower Destiny Survive
- 앞으로 움직이는 타워를 공격하는 몬스터 웨이브를 막아내며 계속해서 전진하는 타워디펜스 전략 게임

#### 필수 기능
1. 이동하는 몬스터들이 올라타면서 쌓이고 순환이 되는 움직임 구현_적 AI
2. 타워 꼭대기 사냥꾼의 샷건 공격 구현
3. 데미지 표기 및 피격 효과 구현
<details>
    <summary>기능 정리</summary>

#### 구현 기능
- Run, Attack, Death, Climb 상태 구현
- Climb : 앞 몬스터를 타고 층을 쌓는 기능 구현
- 세 방향에서 몬스터가 생성되는 것 구현
- 타워 꼭대기 사냥꾼의 샷건 공격 구현
- 데미지 표기 및 피격 효과 구현

#### 구조
- 적 AI                           : FSM
- 몬스터, 총알, 데미지 표기 Text    : 오브젝트 풀링

</details>

### 기술 문서
<details>
    <summary> 자세히 </summary>

#### FSM
- 몬스터의 AI를 구현하기 위해 FSM을 활용하였습니다.
- Run, Climb, Attack, Death 상태를 분리 구현
- [AIStateMachine](https://github.com/PortugaCode/Tower-Destiny-Survive_Portuga/blob/main/Assets/2.Script/EnemyAI/AIStateMachine.cs)
- [AIAgent](https://github.com/PortugaCode/Tower-Destiny-Survive_Portuga/blob/main/Assets/2.Script/EnemyAI/AIAgent.cs)
- [AIState](https://github.com/PortugaCode/Tower-Destiny-Survive_Portuga/blob/main/Assets/2.Script/EnemyAI/AIState.cs)

</details>
