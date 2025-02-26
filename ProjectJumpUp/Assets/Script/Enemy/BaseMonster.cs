using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Idle,
    Patrol,
    Attack
}

public abstract class BaseMonster : MonoBehaviour, IMonster
{
    public EnemyState currentState;
    protected Transform playerTrs;
    protected Vector2 initialPosition;

    protected Rigidbody2D rigid;


    void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        currentState = EnemyState.Patrol;
        initialPosition = transform.position;
        playerTrs = GameObject.Find("Player").GetComponent<Transform>();

        rigid = GetComponent<Rigidbody2D>();

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
            // ������ üũ
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

                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }


    protected virtual void Patrol() // ���͸��� ��Ʈ�� ��ġ �����ؾ���
    {
       
    
    }

    protected virtual void Attack()
    {

    }

    //private bool IsPlayerDetected(RaycastHit2D hit)
    //{
    //    return hit.collider != null && hit.collider.CompareTag("Player");
    //}
}
