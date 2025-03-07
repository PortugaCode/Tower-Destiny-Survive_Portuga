using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    private AIStateMachine stateMachine;

    [Header("StartState")]
    [SerializeField] private AIStateID initalState;



    public Rigidbody2D rig;
    public float moveSpeed = 3.0f;

    private void Awake()
    {
        stateMachine = new AIStateMachine(this);

        #region [State µî·Ï]
        stateMachine.RegsisterState(new AIRunState());
        #endregion

        stateMachine.ChangeState(initalState);
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            stateMachine.ChangeState(AIStateID.Attack);
        }
    }

    private void Update()
    {
        stateMachine.Update();
    }




}
