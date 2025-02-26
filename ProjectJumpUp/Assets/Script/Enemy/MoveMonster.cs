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

        RaycastHit2D playerHit = CheckPlayerDetection();  // 플레이어 감지 체크

        if (playerHit.collider != null)
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
        Debug.Log("Collider: " + hit.collider?.name); // 충돌한 오브젝트의 이름을 출력
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

        RaycastHit2D playerHit = CheckPlayerDetection();

        // 플레이어 감지 여부 확인 (처음 공격을 시작하기 전에 플레이어가 있는지 체크)
        if (playerHit.collider == null)
        {
            Debug.Log("공격중 플레이어 감지 실패");
            // 플레이어가 감지되지 않았다면 공격을 취소하고 패트롤 상태로 전환
            isAttacking = false;
            currentState = EnemyState.Patrol;
            yield break;
        }

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

        if (playerHit.collider == null)
        {
            Debug.Log("공격 종료");
            currentState = EnemyState.Patrol;  // 다시 패트롤로 안넘어가는 문제가 있음
            Invoke("Move", 3);
        }
    }


    private RaycastHit2D CheckPlayerDetection()
    {
        Vector2 traceVec = new Vector2(rigid.position.x, rigid.position.y);  // Raycast 시작 위치

        // 오른쪽과 왼쪽을 동시에 체크
        RaycastHit2D traceHitR = Physics2D.Raycast(traceVec, Vector3.right, 1, LayerMask.GetMask("Player"));
        RaycastHit2D traceHitL = Physics2D.Raycast(traceVec, Vector3.left, 1, LayerMask.GetMask("Player"));

        return (traceHitR.collider != null) ? traceHitR : traceHitL; // 플레이어가 감지된 첫 번째 RaycastHit 반환
    }
}
