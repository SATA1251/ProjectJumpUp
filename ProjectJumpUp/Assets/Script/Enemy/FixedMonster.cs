using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMonster : BaseMonster
{
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private float attackCooldown = 0.2f;
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private float attackRange = 0.2f;
    [SerializeField] private LayerMask playerLayer;

    private float lastAttackTime;
    private bool isAttacking = false;
    private Vector2 targetAttackPosition; // 공격 위치 저장용

    protected override void Patrol() // 몬스터마다 패트롤 위치 지정해야함
    {
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        if (playerTrs == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTrs.position);

        if(distanceToPlayer <= detectionRange && Time.time - lastAttackTime >= attackCooldown && !isAttacking)
        {
            Debug.Log("고정형 몬스터가 플레이어를 공격 시작");
            targetAttackPosition = playerTrs.position;
            Attack();
        }
    }

    protected override void Attack()
    {
        StartCoroutine(AttackLogic());
    }

    private IEnumerator AttackLogic()
    {
        isAttacking = true;

        yield return new WaitForSeconds(attackCooldown);

        // 공격 지점 설정
        if (attackPoint != null)
        {
            attackPoint.position = targetAttackPosition;
        }

        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (hit != null)
        {
            Debug.Log("플레이어 타격");
        }
        else
        {
            Debug.Log("플레이어 타격 실패");
        }

        lastAttackTime = Time.time;
        isAttacking = false;

    }

    // 기즈모 출력
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange); // 공격 범위 시각화
        }

        // 감지 범위 표시 (선택 사항)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
