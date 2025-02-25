using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster : BaseMonster
{
   protected override void Patrol() // 몬스터마다 패트롤 위치 지정해야함
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
        // 공격 중이면 실행하지 않음
        isAttacking = true;

        // 공격 쿨타임 적용
        lastAttackTime = Time.time;

        // 투사체 설정
        Transform projectile = transform.GetChild(0); // 첫 번째 자식 오브젝트(투사체)
        projectile.gameObject.SetActive(true);
        Vector3 startPosition = projectile.position;

        float projectileSpeed = 1f;  // 투사체 속도
        float projectileLifetime = 2f; // 투사체 유지 시간
        float elapsedTime = 0f;

        // 투사체를 일정 시간 동안 이동
        while (elapsedTime < projectileLifetime)
        {
            float moveStep = projectileSpeed * Time.deltaTime;
            projectile.Translate(Vector3.right * moveStep); // 투사체 이동
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 일정 시간이 지나면 투사체 비활성화 및 위치 초기화
        projectile.gameObject.SetActive(false);
        projectile.position = startPosition;

        // 공격 종료
        isAttacking = false;
    }
}
