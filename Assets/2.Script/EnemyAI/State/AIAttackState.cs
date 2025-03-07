using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.Attack;   
    }

    public void Enter(AIAgent agent)
    {
        agent.rig.velocity = Vector2.zero;
    }

    public void AIFixedUpdate(AIAgent agent)
    {
        
    }

    public void AIUpdate(AIAgent agent)
    {
        
    }

    public void Exit(AIAgent agent)
    {
        
    }


}
