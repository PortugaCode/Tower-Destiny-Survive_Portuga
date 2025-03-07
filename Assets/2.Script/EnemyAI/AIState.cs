using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AIStateID
{
    None = -1,
    Run,
    Climb,
    Attack,
    Death,
}


public interface AIState
{
    AIStateID GetID();
    void Enter(AIAgent agent);
    void AIUpdate(AIAgent agent);
    void AIFixedUpdate(AIAgent agent);
    void Exit(AIAgent agent);
}
