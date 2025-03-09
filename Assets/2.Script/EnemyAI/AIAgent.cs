
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    private AIStateMachine stateMachine;
    public AIStateMachine StateMachine
    {
        get { return stateMachine; }
    }

    public AIStateID GetCurrentState()
    {
        return currentState;
    }

    [Header("StartState")]
    [SerializeField] private AIStateID initalState;
    [SerializeField] private AIStateID currentState;

    [Header("Rigidbody2D")]
    public Rigidbody2D rig;

    [Header("Check")]
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Transform groundPoint;


    [Header("SpeedValue")]
    public float moveSpeed = 100.0f;
    public float verticalSpeed = 110.0f;

    public bool isStepping = true;
    public bool isGround = true;

    public bool CanClimb = false;
    public bool isClimb = false;

    private void Awake()
    {
        stateMachine = new AIStateMachine(this);

        #region [State 등록]
        stateMachine.RegsisterState(new AIRunState());
        stateMachine.RegsisterState(new AIAttackState());
        stateMachine.RegsisterState(new AIClimbState());
        #endregion

        stateMachine.ChangeState(initalState);
        currentState = initalState;
    }


    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && currentState != AIStateID.Attack)
        {
            ChangeCurrentState(AIStateID.Attack);
            stateMachine.ChangeState(AIStateID.Attack);

            return;
        }

        if (collision.collider.CompareTag("Enemy") && currentState != AIStateID.Climb)
        {
            AIAgent target;
            target = collision.gameObject.GetComponent<AIAgent>();

            CanClimb =
                collision.contacts[0].normal.x <= 1.0f &&
                collision.contacts[0].normal.x >= 0.5f &&
                target.isGround == true &&
                target.isStepping == true &&
                isStepping;

            if (CanClimb)
            {
                isClimb = true;
                Debug.Log("climb으로 변경");
                ChangeCurrentState(AIStateID.Climb);
                stateMachine.ChangeState(AIStateID.Climb);
                return;
            }

            isClimb = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            ChangeCurrentState(AIStateID.Run);
            stateMachine.ChangeState(AIStateID.Run);

            return;
        }

        if (collision.collider.CompareTag("Enemy") && currentState == AIStateID.Climb)
        {
            ChangeCurrentState(AIStateID.Run);
            stateMachine.ChangeState(AIStateID.Run);

            return;
        }
    }

    private void Update()
    {
        stateMachine.Update();

        RaycastHit2D raycastHit2D_Stepping = Physics2D.Raycast((Vector2)rayPoint.position, Vector2.up, 1f);
        if(raycastHit2D_Stepping)
        {
            if (raycastHit2D_Stepping.collider.CompareTag("Enemy")) isStepping = false;
        }else
        isStepping = true;

        RaycastHit2D raycastHit2D_Ground = Physics2D.Raycast((Vector2)groundPoint.position, Vector2.down, 0.01f);
        if (raycastHit2D_Ground)
        {
            isGround = true;
        }
        else
            isGround = false;
    }

    public void ChangeCurrentState(AIStateID aIStateID)
    {
        this.currentState = aIStateID;
    }



}
