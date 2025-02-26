using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster : BaseMonster
{
    public int nextMove;

    private bool isAttacking = false;
    private float lastAttackTime = 3f;

    RaycastHit2D traceHitR;
    RaycastHit2D traceHitL;

    public override void Initialize()
    {
        currentState = EnemyState.Patrol;
        initialPosition = transform.position;
        playerTrs = GameObject.Find("Player").GetComponent<Transform>();

        rigid = GetComponent<Rigidbody2D>();

        Invoke("Move", 3);
    }

    void Move()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("Move", 3);
    }


    protected override void Patrol() // 몬스터마다 패트롤 위치 지정해야함
    {
        // 공격 중에는 이동을 멈춤
        if (currentState == EnemyState.Attack)
            return;

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Vector2 traceVec = new Vector2(rigid.position.x, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        Debug.DrawRay(traceVec, Vector3.right, new Color(1, 0, 0));
        Debug.DrawRay(traceVec, Vector3.left, new Color(0, 0, 1));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform")); // 여기를 몬스터 플랫폼으로 해야할까?


        traceHitR = Physics2D.Raycast(traceVec, Vector3.right, 1, LayerMask.GetMask("Player"));
        traceHitL = Physics2D.Raycast(traceVec, Vector3.left, 1, LayerMask.GetMask("Player"));

        if (IsPlayerDetected(traceHitR) || IsPlayerDetected(traceHitL))
        {
            Debug.Log("공격 시작");
            CancelInvoke("Move");
            currentState = EnemyState.Attack;
        }
        else if (rayHit.collider == null && currentState != EnemyState.Attack)
        {
            nextMove *= -1;
            CancelInvoke("Move");
            Invoke("Move", 3);
        }
    }

    protected override void Attack()
    {
        StartCoroutine(AttackLogic());
    }

    private bool IsPlayerDetected(RaycastHit2D hit)
    {
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    IEnumerator AttackLogic()
    {
        // 공격 중이면 실행하지 않음
        if (isAttacking) yield break;
        isAttacking = true;

        // 공격 쿨타임 적용
        lastAttackTime = Time.time;

        // 투사체 설정
        Transform projectile = transform.GetChild(0); // 첫 번째 자식 오브젝트(투사체)
        Vector3 startPosition = projectile.localPosition;

        float projectileSpeed = 1.0f;  // 투사체 속도
        float projectileLifetime = 3f; // 투사체 유지 시간
        float elapsedTime = 0f;

        float direction = nextMove;

        projectile.gameObject.SetActive(true);

        // 투사체를 일정 시간 동안 이동
        while (elapsedTime < projectileLifetime)
        {
            float moveStep = projectileSpeed * Time.deltaTime;
            projectile.Translate(Vector3.right * moveStep * direction, Space.Self); // 투사체 이동
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 일정 시간이 지나면 투사체 비활성화 및 위치 초기화
        projectile.localPosition = startPosition;
        projectile.gameObject.SetActive(false);

        // 공격 종료
        isAttacking = false;

        if (!(IsPlayerDetected(traceHitR) || IsPlayerDetected(traceHitL)))
        {
            currentState = EnemyState.Patrol;
            Invoke("Move", 3);
        }
    }
}
