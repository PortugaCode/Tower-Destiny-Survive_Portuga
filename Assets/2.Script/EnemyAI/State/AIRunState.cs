using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRunState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.Run;
    }

    public void Enter(AIAgent agent)
    {
        //todo
    }

    public void AIFixedUpdate(AIAgent agent)
    {
        //todo
        agent.rig.MovePosition(agent.rig.position + Vector2.left * agent.moveSpeed * Time.deltaTime);
    }

    public void AIUpdate(AIAgent agent)
    {
        //todo
    }

    public void Exit(AIAgent agent)
    {
        //todo
    }
}
