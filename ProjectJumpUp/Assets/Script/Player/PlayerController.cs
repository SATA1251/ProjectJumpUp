using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector2 dragStartPosition; // �巡�� ���� ��ġ
    private Vector2 currentDragPosition; // �巡�� ���� ��ġ
    Rigidbody2D rb;
    private LineRenderer lineRenderer;

    [SerializeField]
    private float throwForce = 1f;

    public float discountForce = 3;
    public float discountThrow = 100;

    private bool isDragging = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                dragStartPosition = Camera.main.ScreenToWorldPoint(touch.position);
                lineRenderer.enabled = true;
            }
           else if (touch.phase == TouchPhase.Moved)
            {
                currentDragPosition = Camera.main.ScreenToWorldPoint(touch.position);

                Vector2 position2D = new Vector2(transform.position.x, transform.position.y);

                Vector2 dragVector = dragStartPosition - currentDragPosition;

                float dragDistance = dragVector.magnitude; // ������ ����
                Vector2 throwForceVector = dragVector.normalized * dragDistance * throwForce / discountThrow;


                lineRenderer.SetPosition(0, position2D); // ��������� ����
                lineRenderer.SetPosition(1, throwForceVector);

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 dragVector = dragStartPosition - currentDragPosition;
                ThrowObject(dragVector);

                lineRenderer.enabled = false;
            }
        }
    }


    private void ThrowObject(Vector2 direction)  // ���� �����θ� ����
    {
        rb.velocity = Vector2.zero;

        float dragDistance = direction.magnitude; // ������ ����
        Vector2 throwForceVector = direction.normalized * dragDistance * throwForce / discountForce;

        //�� ����
        rb.AddForce(throwForceVector, ForceMode2D.Impulse);
    }
}
