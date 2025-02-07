using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPlatform : BasePlatform
{
    private Vector2 initialPosition;

    private void Start()
    {
        initialPosition = transform.position; // 시작 위치 저장
    }

    public override void Respawn()
    {
        base.Respawn();
        transform.position = initialPosition; // 원래 위치로 되돌리기
    }
}
