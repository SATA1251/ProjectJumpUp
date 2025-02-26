using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector2 dragStartPosition; // 드래그 시작 위치
    private Vector2 currentDragPosition; // 드래그 종료 위치
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
        lineRenderer.positionCount = 2; // 시작점과 끝점 표시
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

                float dragDistance = Mathf.Min(dragVector.magnitude, maxDagDistance); // 벡터의 길이

                lineRenderer.SetPosition(0, position2D); // 문제생기면 수정
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

        if (IsMovingUp())
        {
            IgnorPlatfomCollisions(true); // 발판과 충돌 무시
        }
        else if (!IsMovingUp())
        {
            IgnorPlatfomCollisions(false); // 발판과 충돌
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) // platform 태그 확인
        {
            if (isOnPlatform == false)
            {
                isJumping = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) // 플랫폼에서 벗어남
        {
            isJumping = true;
            //lineRenderer.enabled = false;
        }
    }

    private void ThrowObject(Vector2 direction)
    {
        //rb.velocity = Vector2.zero;
        //float dragDistance = direction.magnitude; // 벡터의 길이
        //Vector2 throwForceVector = direction.normalized * dragDistance * throwForce;
        ////힘 적용
        //rb.AddForce(throwForceVector, ForceMode2D.Impulse);

        rb.velocity = Vector2.zero;

        // 드래그 거리 계산 (최대값 적용)
        float dragDistance = Mathf.Min(direction.magnitude, maxDagDistance);

        // 최소 및 최대 던지는 힘 설정
        float minThrowForce = 2f;
        float maxThrowForce = 8f;

        // 드래그 거리 비율을 이용해 선형적으로 힘 조절
        float scaledThrowForce = Mathf.Lerp(minThrowForce, maxThrowForce, dragDistance / maxDagDistance);

        // 던지는 힘 벡터 계산 (정규화된 방향 * 조정된 힘)
        Vector2 throwForceVector = direction.normalized * scaledThrowForce;

        // 힘 적용
        rb.AddForce(throwForceVector, ForceMode2D.Impulse);
    }

    private void IgnorPlatfomCollisions(bool ignore)
    {
            Collider2D[] platformColliders = GameObject.FindGameObjectsWithTag("Platform")
            .Select(platform => platform.GetComponent<Collider2D>())
            .Where(collider => collider != null) //  null 체크 추가
            .ToArray();

        foreach( Collider2D platformCollider in platformColliders )
        {
            if (playerCollider != null && platformCollider != null)
            {
                Physics2D.IgnoreCollision(playerCollider, platformCollider, ignore);
            }
        }

        isOnPlatform = ignore;
    }

    public bool IsMovingUp()
    {
        return rb.velocity.y > 0;
    }
}
