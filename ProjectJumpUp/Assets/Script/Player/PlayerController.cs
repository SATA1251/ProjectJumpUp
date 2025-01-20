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

    public float discountForce = 3;
    public float discountThrow = 100;
    // Start is called before the first frame update
    void Start()
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
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                dragStartPosition = Camera.main.ScreenToWorldPoint(touch.position);
                
            }
           else if (touch.phase == TouchPhase.Moved)
            {
                float maxDagDistance = 2.5f;

                currentDragPosition = Camera.main.ScreenToWorldPoint(touch.position);

                Vector2 position2D = new Vector2(transform.position.x, transform.position.y);

                Vector2 dragVector = dragStartPosition - currentDragPosition;

                float dragDistance = Mathf.Min(dragVector.magnitude, maxDagDistance); // 벡터의 길이

                lineRenderer.SetPosition(0, position2D); // 문제생기면 수정
                lineRenderer.SetPosition(1, position2D + (Vector2)dragVector.normalized * dragDistance);
                lineRenderer.enabled = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 dragVector = dragStartPosition - currentDragPosition;
                ThrowObject(dragVector);
                lineRenderer.enabled = false;
            }
        }

        if (rb.velocity.y > 0)
        {
            IgnorPlatfomCollisions(true); // 발판과 충돌 무시
        }
        else if (rb.velocity.y <= 0)
        {
            IgnorPlatfomCollisions(false); // 발판과 충돌
        }
    }

    private void ThrowObject(Vector2 direction)
    {
        rb.velocity = Vector2.zero;
        float dragDistance = direction.magnitude; // 벡터의 길이
        Vector2 throwForceVector = direction.normalized * dragDistance * throwForce;
        //힘 적용
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
    }
}
