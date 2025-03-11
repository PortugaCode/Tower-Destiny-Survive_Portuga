using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDieState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.Death;
    }

    public void Enter(AIAgent agent)
    {
        agent.rig.velocity = Vector3.zero;
        agent.animator.SetBool("IsDead", true);

        
        //SpawnManager.Instance.EnqueueData(agent.PoolUniqueID, agent.gameObject);
    }

    public void AIFixedUpdate(AIAgent agent)
    {
        
    }

    public void AIUpdate(AIAgent agent)
    {

    }


    public void Exit(AIAgent agent)
    {
        agent.animator.SetBool("IsDead", false);
    }


}
