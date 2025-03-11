email : poolutoocaa123@gmail.com

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
- 몬스터의 AI를 구현하기 위해 FSM을 활용
- Run, Climb, Attack, Death 상태를 분리 구현
- [AIStateMachine](https://github.com/PortugaCode/Tower-Destiny-Survive_Portuga/blob/main/Assets/2.Script/EnemyAI/AIStateMachine.cs)
- [AIAgent](https://github.com/PortugaCode/Tower-Destiny-Survive_Portuga/blob/main/Assets/2.Script/EnemyAI/AIAgent.cs)
- [AIState](https://github.com/PortugaCode/Tower-Destiny-Survive_Portuga/blob/main/Assets/2.Script/EnemyAI/AIState.cs)

<br></br>
#### ObjectPooling
- 몬스터, 총알, 데미지 표기 Text는 반복적으로 사용하기 때문에 오브젝트 풀링으로 성능을 높임
- ObjectPoolData SO에 오브젝트 풀링할 Prefab을 추가하고 SpawnManager(ObjectPooling 담당)에서 사용
- [ObjectPoolData](https://github.com/PortugaCode/Tower-Destiny-Survive_Portuga/blob/main/Assets/2.Script/SpawnManager/ObjectPoolData.cs)
- [SpawnManager](https://github.com/PortugaCode/Tower-Destiny-Survive_Portuga/blob/main/Assets/2.Script/SpawnManager/SpawnManager.cs)
 
<br></br>
#### MonsterTower
- 몬스터가 보다 자연스럽게 타워를 올라가게 하기 위해 계단 형태로 탑을 쌓도록 구현
- 물리 충돌 및 RayCast를 활용하여 앞의 몬스터가 올라갈 수 있는 상태인지 체크

<br></br>
#### PlayerAttack
- 총의 총구는 마우스 커서 위치를 향하도록 구현
- 클릭 시 해당 총구 방향에서 세 갈래로 발사되도록 구현
- 몬스터 피격 시 일정 데미지를 주도록 구현
- 피격 이펙트, 데미지 텍스트, 체력 바 UI 구현

<br></br>
####  EndlessBackground
- Player가 앞으로 가는 듯한 효과를 위해 배경이 무한히 움직이며 반복되는 기능 구현

</details>
