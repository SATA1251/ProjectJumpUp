using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster : BaseMonster
{
   protected override void Patrol() // ���͸��� ��Ʈ�� ��ġ �����ؾ���
    {
        float moveDirection = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * moveDirection * patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(initialPosition, transform.position) >= patrolRange)
        {
            movingRight = !movingRight;
        }
    }

    protected override void Attack()
    {
        if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
        {
            StartCoroutine(AttackLogic());
        }
    }

    IEnumerator AttackLogic()
    {
        // ���� ���̸� �������� ����
        isAttacking = true;

        // ���� ��Ÿ�� ����
        lastAttackTime = Time.time;

        // ����ü ����
        Transform projectile = transform.GetChild(0); // ù ��° �ڽ� ������Ʈ(����ü)
        projectile.gameObject.SetActive(true);
        Vector3 startPosition = projectile.position;

        float projectileSpeed = 1f;  // ����ü �ӵ�
        float projectileLifetime = 2f; // ����ü ���� �ð�
        float elapsedTime = 0f;

        // ����ü�� ���� �ð� ���� �̵�
        while (elapsedTime < projectileLifetime)
        {
            float moveStep = projectileSpeed * Time.deltaTime;
            projectile.Translate(Vector3.right * moveStep); // ����ü �̵�
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� �ð��� ������ ����ü ��Ȱ��ȭ �� ��ġ �ʱ�ȭ
        projectile.gameObject.SetActive(false);
        projectile.position = startPosition;

        // ���� ����
        isAttacking = false;
    }
}
