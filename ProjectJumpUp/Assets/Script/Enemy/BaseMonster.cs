using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum EnemyState
{
    Idle,
    Patrol,
    Attack
}

public abstract class BaseMonster : MonoBehaviour, IMonster
{
    private EnemyState currentState;
    protected float patrolSpeed = 1f;
    protected float patrolRange = 2f;
    protected float sightRange = 1f;
    protected float attackCooldown = 2f;
    protected Transform playerTrs;

    protected Vector2 initialPosition;
    protected bool movingRight = true;
    protected bool isAttacking = false;
    protected float lastAttackTime;

    void Start()
    {
        Initialize();    
    }

    public virtual void Initialize()
    {
        currentState = EnemyState.Patrol;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterAi();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 데미지 체크
        }
    }

    void MonsterAi()
    {
        switch (currentState)
        {
            case EnemyState.Idle:

                break;
            case EnemyState.Patrol:
                Patrol();
                if (PlayerInSight())
                {
                    currentState = EnemyState.Attack;
                }
                break;
            case EnemyState.Attack:
                if (!PlayerInSight())
                {
                    currentState = EnemyState.Patrol;
                }
                else
                {
                    Attack();
                }
                break;
        }
    }

    bool PlayerInSight()
    {
        return Mathf.Abs(transform.position.x - playerTrs.position.x) <= sightRange;
    }

    protected virtual void Patrol() // 몬스터마다 패트롤 위치 지정해야함
    {

    }

    protected virtual void Attack()
    {

    }
}
