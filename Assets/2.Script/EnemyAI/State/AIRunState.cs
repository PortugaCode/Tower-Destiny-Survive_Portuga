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
        agent.animator.SetBool("IsIdle", true);
    }

    public void AIFixedUpdate(AIAgent agent)
    {
        if (agent.rig.velocity.y > 0f && agent.isClimb == false)
        {
            agent.rig.velocity = new Vector2(-1.0f * agent.moveSpeed * Time.fixedDeltaTime, agent.rig.velocity.y * 0.45f);
            return;
        }

        agent.rig.velocity = new Vector2(-1.0f * agent.moveSpeed * Time.fixedDeltaTime, agent.rig.velocity.y);


    }

    public void AIUpdate(AIAgent agent)
    {
        
    }

    public void Exit(AIAgent agent)
    {
        agent.animator.SetBool("IsIdle", false);
    }
}
