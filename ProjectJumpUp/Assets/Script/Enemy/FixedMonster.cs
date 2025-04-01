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
    private Vector2 targetAttackPosition; // ���� ��ġ �����

    protected override void Patrol() // ���͸��� ��Ʈ�� ��ġ �����ؾ���
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
            Debug.Log("������ ���Ͱ� �÷��̾ ���� ����");
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

        // ���� ���� ����
        if (attackPoint != null)
        {
            attackPoint.position = targetAttackPosition;
        }

        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (hit != null)
        {
            Debug.Log("�÷��̾� Ÿ��");
        }
        else
        {
            Debug.Log("�÷��̾� Ÿ�� ����");
        }

        lastAttackTime = Time.time;
        isAttacking = false;

    }

    // ����� ���
    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange); // ���� ���� �ð�ȭ
        }

        // ���� ���� ǥ�� (���� ����)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
