using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatform : BasePlatform
{
    // 기존 방법을 가져와서 쓰기

    public override void Respawn()
    {
        base.isSteppendOn = false;
        ObjectPool pool = FindObjectOfType<ObjectPool>();
        pool.ReturnObject(gameObject);
    }
}
