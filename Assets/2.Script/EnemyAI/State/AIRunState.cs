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

    }

    public void AIFixedUpdate(AIAgent agent)
    {

        agent.rig.velocity = new Vector2(-1.0f * agent.moveSpeed * Time.fixedDeltaTime, agent.rig.velocity.y);
       
    }

    public void AIUpdate(AIAgent agent)
    {
        if(agent.rig.velocity.y > 0f && agent.isClimb == false)
        {
            agent.rig.velocity = new Vector2(-1.0f * agent.moveSpeed * Time.fixedDeltaTime, agent.rig.velocity.y * 0.45f);
        }
    }

    public void Exit(AIAgent agent)
    {

    }
}
