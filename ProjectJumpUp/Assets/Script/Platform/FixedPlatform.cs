using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPlatform : BasePlatform
{
    private Vector2 initialPosition;

    private void Start()
    {
        initialPosition = transform.position; // ���� ��ġ ����
    }

    public override void Respawn()
    {
        base.Respawn();
        transform.position = initialPosition; // ���� ��ġ�� �ǵ�����
    }
}
