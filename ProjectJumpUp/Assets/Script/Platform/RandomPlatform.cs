using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatform : BasePlatform
{
    // ���� ����� �����ͼ� ����

    public override void Respawn()
    {
        base.isSteppendOn = false;
        ObjectPool pool = FindObjectOfType<ObjectPool>();
        pool.ReturnObject(gameObject);
    }
}
