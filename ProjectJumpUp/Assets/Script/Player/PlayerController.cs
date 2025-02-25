using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector2 dragStartPosition; // �巡�� ���� ��ġ
    private Vector2 currentDragPosition; // �巡�� ���� ��ġ
    Rigidbody2D rb;
    private Collider2D playerCollider;
    private LineRenderer lineRenderer;

    [SerializeField]
    private float throwForce = 1f;

    public float discountForce = 5;
    public float discountThrow = 100;

    private bool isJumping = false;
    public bool isOnPlatform = false;

   private float maxDagDistance = 2.5f;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<Collider2D>();

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // �������� ���� ǥ��
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
    }

    void PlayerJump()
    {
        if(!isJumping && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                dragStartPosition = Camera.main.ScreenToWorldPoint(touch.position);
                
            }
           else if (touch.phase == TouchPhase.Moved && isJumping == false)
            {
                
                currentDragPosition = Camera.main.ScreenToWorldPoint(touch.position);

                Vector2 position2D = new Vector2(transform.position.x, transform.position.y);

                Vector2 dragVector = dragStartPosition - currentDragPosition;

                float dragDistance = Mathf.Min(dragVector.magnitude, maxDagDistance); // ������ ����

                lineRenderer.SetPosition(0, position2D); // ��������� ����
                lineRenderer.SetPosition(1, position2D + (Vector2)dragVector.normalized * dragDistance);
                lineRenderer.enabled = true;
            }
            else if (touch.phase == TouchPhase.Ended && isJumping == false)
            {
                Vector2 dragVector = dragStartPosition - currentDragPosition;
                ThrowObject(dragVector);
                lineRenderer.enabled = false;
                isJumping = true;
            }
        }

        if (rb.velocity.y > 0)
        {
            IgnorPlatfomCollisions(true); // ���ǰ� �浹 ����
        }
        else if (rb.velocity.y <= 0)
        {
            IgnorPlatfomCollisions(false); // ���ǰ� �浹
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) // platform �±� Ȯ��
        {
            if (isOnPlatform == false)
            {
                isJumping = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) // �÷������� ���
        {
            isJumping = true;
            //lineRenderer.enabled = false;
        }
    }

    private void ThrowObject(Vector2 direction)
    {
        //rb.velocity = Vector2.zero;
        //float dragDistance = direction.magnitude; // ������ ����
        //Vector2 throwForceVector = direction.normalized * dragDistance * throwForce;
        ////�� ����
        //rb.AddForce(throwForceVector, ForceMode2D.Impulse);

        rb.velocity = Vector2.zero;

        // �巡�� �Ÿ� ��� (�ִ밪 ����)
        float dragDistance = Mathf.Min(direction.magnitude, maxDagDistance);

        // �ּ� �� �ִ� ������ �� ����
        float minThrowForce = 2f;
        float maxThrowForce = 8f;

        // �巡�� �Ÿ� ������ �̿��� ���������� �� ����
        float scaledThrowForce = Mathf.Lerp(minThrowForce, maxThrowForce, dragDistance / maxDagDistance);

        // ������ �� ���� ��� (����ȭ�� ���� * ������ ��)
        Vector2 throwForceVector = direction.normalized * scaledThrowForce;

        // �� ����
        rb.AddForce(throwForceVector, ForceMode2D.Impulse);
    }

    private void IgnorPlatfomCollisions(bool ignore)
    {
            Collider2D[] platformColliders = GameObject.FindGameObjectsWithTag("Platform")
            .Select(platform => platform.GetComponent<Collider2D>())
            .ToArray();

        foreach( Collider2D platformCollider in platformColliders )
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, ignore);
        }

        isOnPlatform = ignore;
    }
}
