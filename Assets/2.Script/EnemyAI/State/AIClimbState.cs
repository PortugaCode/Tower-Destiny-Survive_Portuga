using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIClimbState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.Climb;
    }
    public void Enter(AIAgent agent)
    {
        //agent.rig.gravityScale = 0f;
    }
    public void AIFixedUpdate(AIAgent agent)
    {
        agent.rig.velocity = new Vector2(-1.0f * agent.moveSpeed * Time.fixedDeltaTime, 1.0f * agent.verticalSpeed * Time.fixedDeltaTime);
    }

    public void AIUpdate(AIAgent agent)
    {

    }

    public void Exit(AIAgent agent)
    {
        //agent.rig.gravityScale = 1f;
    }
}
