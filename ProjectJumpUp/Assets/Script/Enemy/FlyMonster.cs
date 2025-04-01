using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMonster : MoveMonster
{
    private float moveSpeed = 1f;
    private float moveRange = 2f;

    public override void Initialize()
    {
        currentState = EnemyState.Patrol;
        initialPosition = transform.position;
        playerTrs = GameObject.Find("Player").GetComponent<Transform>();

        rigid = GetComponent<Rigidbody2D>();

        rigid.gravityScale = 0;
        rigid.isKinematic = true;

        Invoke("Move", 3);

    }

    protected override void Move()
    {
        if (direction == 0)
        {
            direction = (Random.value < 0.5f) ? -1 : 1;
        }
    }

    protected override void Patrol() // 몬스터마다 패트롤 위치 지정해야함
    {
        // 공격 중에는 이동을 멈춤
        if (currentState == EnemyState.Attack)
            return;

        if(Mathf.Abs(transform.position.x - initialPosition.x) >= moveRange)
        {
            direction *= -1;
        }

        transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);

        Vector2 traceVec = new Vector2(rigid.position.x, rigid.position.y);

        Debug.DrawRay(traceVec, Vector3.right, new Color(1, 0, 0));
        Debug.DrawRay(traceVec, Vector3.left, new Color(0, 0, 1));

 

        RaycastHit2D playerHit = CheckPlayerDetection();  // 플레이어 감지 체크

        if (playerHit.collider != null)
        {
            Debug.Log("공격 시작");
            currentState = EnemyState.Attack;
        }
    }


}
