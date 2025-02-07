using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatform : BasePlatform
{
    PlatformSpawner platformSpawner;
    ObjectPool pool;
    private void Start()
    {
        platformSpawner = GameObject.FindWithTag("PlatformSpawner").GetComponent<PlatformSpawner>();
        pool = FindObjectOfType<ObjectPool>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }
    // 기존 방법을 가져와서 쓰기
    public override void Disappear()
    {
        // gameObject.SetActive(false);  // 기본적으로 1초 후 비활성화
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        if (boxCollider2d != null)
        {
            boxCollider2d.enabled = false;
        }


        platformSpawner.platformNumberDiscount();
        pool.ReturnObject(gameObject);
        Debug.Log("랜덤플랫폼에서 비활성화 성공");
       // StartCoroutine(RespawnAfterDelay());
    }



    public override void Respawn()
    {
        isSteppendOn = false;
    }

    public void RespawnRandom()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        if (boxCollider2d != null)
        {
            boxCollider2d.enabled = true;
        }
    }
}
