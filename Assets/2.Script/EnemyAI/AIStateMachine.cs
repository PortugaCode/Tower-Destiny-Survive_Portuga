using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    private AIState[] states;
    private AIAgent agent;
    private AIStateID currentState;

    // Init Setting
    public AIStateMachine(AIAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AIStateID)).Length;
        states = new AIState[numStates];
    }


    // ���� ��� �޼���
    public void RegsisterState(AIState state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }

    // ���� ã�� �޼���
    public AIState GetState(AIStateID stateID)
    {
        int index = (int)stateID;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.AIUpdate(agent);
    }

    public void FixedUpdate()
    {
        GetState(currentState)?.AIFixedUpdate(agent);
    }

    public void ChangeState(AIStateID newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
