
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

    [Header("Aniamator")]
    public Animator animator;

    [Header("EnemyID")]
    [SerializeField] private PoolUniqueID poolUniqueID;
    public PoolUniqueID PoolUniqueID => poolUniqueID;
    [SerializeField] private LayerMask layerMask;

    [Header("RayPoint")]
    [SerializeField] private Transform rayPoint;
    [SerializeField] private Transform groundPoint;

    [Header("Health")]
    public float health = 100.0f;
    public float health_Full = 100.0f;


    [Header("SpeedValue")]
    public float moveSpeed = 100.0f;
    public float verticalSpeed = 110.0f;

    [Header("CheckState")]
    public bool isStepping = true;
    public bool isGround = true;
    public bool CanClimb = false;
    public bool isClimb = false;
    private bool isEnd = false;


    [Header("Effect")]
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private ParticleSystem deathEffect;
    public ParticleSystem DeathEffect => deathEffect;


    public Action<float> OnDamage;

    private void Awake()
    {
        stateMachine = new AIStateMachine(this);

        #region [State ���]
        stateMachine.RegsisterState(new AIRunState());
        stateMachine.RegsisterState(new AIAttackState());
        stateMachine.RegsisterState(new AIClimbState());
        stateMachine.RegsisterState(new AIDieState());
        #endregion

        stateMachine.ChangeState(initalState);
        currentState = initalState;
    }

    private void OnEnable()
    {
        health = health_Full;
        stateMachine.ChangeState(initalState);
        currentState = initalState;
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (health <= 0) return;

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
                target.isEnd == false &&
                isStepping;

            if (CanClimb)
            {
                isClimb = true;
                ChangeCurrentState(AIStateID.Climb);
                stateMachine.ChangeState(AIStateID.Climb);
                return;
            }

            isClimb = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (health <= 0) return;



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

        if (currentState == AIStateID.Death) return;

        #region [Raycast �κ�]

        RaycastHit2D raycastHit2D_Stepping = Physics2D.Raycast((Vector2)rayPoint.position, Vector2.up, 1f, layerMask);
        if(raycastHit2D_Stepping)
        {
            if (raycastHit2D_Stepping.collider.CompareTag("Enemy")) isStepping = false;
        }
        else
            isStepping = true;

        RaycastHit2D raycastHit2D_Hero = Physics2D.Raycast((Vector2)rayPoint.position, Vector2.left, 1f, layerMask);
        if (raycastHit2D_Hero)
        {
            if (raycastHit2D_Hero.collider.CompareTag("Hero")) isEnd = true;
        }
        else
            isEnd = false;

        RaycastHit2D raycastHit2D_Ground = Physics2D.Raycast((Vector2)groundPoint.position, Vector2.down, 0.01f, layerMask);
        if (raycastHit2D_Ground)
        {
            isGround = true;
        }
        else
            isGround = false;

        #endregion
    }

    public void ChangeCurrentState(AIStateID aIStateID)
    {
        this.currentState = aIStateID;
    }

    public void OnDieAction()
    {
        SpawnManager.Instance.EnqueueData(poolUniqueID, this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        flashEffect.Flash();

        OnDamage?.Invoke(damage);

        if (health <= 0)
        {
            deathEffect.Play();
            ChangeCurrentState(AIStateID.Death);
            stateMachine.ChangeState(AIStateID.Death);
            return;
        }
    }


}
