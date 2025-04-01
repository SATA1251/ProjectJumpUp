using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster : BaseMonster
{
    protected int direction = 0;

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

    protected virtual void Move()
    {
        if (direction == 0)
        {
            direction = (Random.value < 0.5f) ? -1 : 1;
        }

        Invoke("Move", 3);
    }


    protected override void Patrol() // ���͸��� ��Ʈ�� ��ġ �����ؾ���
    {
        // ���� �߿��� �̵��� ����
        if (currentState == EnemyState.Attack)
            return;

        rigid.velocity = new Vector2(direction, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + direction * 0.3f, rigid.position.y);
        Vector2 traceVec = new Vector2(rigid.position.x, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        Debug.DrawRay(traceVec, Vector3.right, new Color(1, 0, 0));
        Debug.DrawRay(traceVec, Vector3.left, new Color(0, 0, 1));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 0.3f, LayerMask.GetMask("Platform")); // ���⸦ ���� �÷������� �ؾ��ұ�?

        RaycastHit2D playerHit = CheckPlayerDetection();  // �÷��̾� ���� üũ

        if (playerHit.collider != null)
        {
            Debug.Log("���� ����");
            CancelInvoke("Move");
            currentState = EnemyState.Attack;
        }
        else if (rayHit.collider == null && currentState != EnemyState.Attack)
        {
            direction *= -1;
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
        Debug.Log("Collider: " + hit.collider?.name); // �浹�� ������Ʈ�� �̸��� ���
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    private IEnumerator AttackLogic()
    {
        // ���� ���̸� �������� ����
        if (isAttacking) yield break;
        isAttacking = true;

        // ���� ��Ÿ�� ����
        lastAttackTime = Time.time;

        // ����ü ����
        Transform projectile = transform.GetChild(0); // ù ��° �ڽ� ������Ʈ(����ü)
        Vector3 startPosition = projectile.localPosition;

        float projectileSpeed = 1.0f;  // ����ü �ӵ�
        float projectileLifetime = 3f; // ����ü ���� �ð�
        float elapsedTime = 0f;

        rigid.velocity = Vector2.zero;

        RaycastHit2D playerHit = CheckPlayerDetection();

        // �÷��̾� ���� ���� Ȯ�� (ó�� ������ �����ϱ� ���� �÷��̾ �ִ��� üũ)
        if (playerHit.collider == null)
        {
            Debug.Log("������ �÷��̾� ���� ����");
            // �÷��̾ �������� �ʾҴٸ� ������ ����ϰ� ��Ʈ�� ���·� ��ȯ
            isAttacking = false;
            currentState = EnemyState.Patrol;
            yield break;
        }

        projectile.gameObject.SetActive(true);

        // ����ü�� ���� �ð� ���� �̵�
        while (elapsedTime < projectileLifetime)
        {
            float moveStep = projectileSpeed * Time.deltaTime;
            projectile.Translate(Vector3.right * moveStep * direction, Space.Self); // ����ü �̵�
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� �ð��� ������ ����ü ��Ȱ��ȭ �� ��ġ �ʱ�ȭ
        projectile.localPosition = startPosition;
        projectile.gameObject.SetActive(false);

        // ���� ����
        isAttacking = false;

        if (playerHit.collider == null)
        {
            Debug.Log("���� ����");
            currentState = EnemyState.Patrol;  // �ٽ� ��Ʈ�ѷ� �ȳѾ�� ������ ����
            Invoke("Move", 3);
        }
    }


    protected virtual RaycastHit2D CheckPlayerDetection()
    {
        Vector2 traceVec = new Vector2(rigid.position.x, rigid.position.y);  // Raycast ���� ��ġ

        Vector2 directionVec = (direction == 1) ? Vector2.right : Vector2.left;


        RaycastHit2D traceHit = Physics2D.Raycast(traceVec, directionVec, 1, LayerMask.GetMask("Player"));

        // �����ʰ� ������ ���ÿ� üũ
        //RaycastHit2D traceHitR = Physics2D.Raycast(traceVec, Vector3.right, 1, LayerMask.GetMask("Player"));
        //RaycastHit2D traceHitL = Physics2D.Raycast(traceVec, Vector3.left, 1, LayerMask.GetMask("Player"));

        return traceHit; // �÷��̾ ������ ù ��° RaycastHit ��ȯ
    }
}
