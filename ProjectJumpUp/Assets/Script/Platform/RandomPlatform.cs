using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlatform : BasePlatform
{
    PlatformSpawner platformSpawner;
    ObjectPool pool;
    private float timer;
    private float disappearTime;

    public override void Initialize()
    {
        platformSpawner = GameObject.FindWithTag("PlatformSpawner").GetComponent<PlatformSpawner>();
        pool = FindObjectOfType<ObjectPool>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        //playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        timer = 0;
        disappearTime = 15;
    }


    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= disappearTime)
        {
            Disappear();
            timer = 0;
        }

    }


    // 기존 방법을 가져와서 쓰기
    public override void Disappear()
    {
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
    }

    public override void Respawn()
    {
        isSteppendOn = false;
        isOnPlayer = false;
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
   
        // 플레이어가 위로 이동 중이면 충돌 무시 다시 적용
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null && boxCollider2d != null)
        {
            if (player.IsMovingUp()) // 플레이어가 점프 중인지 확인
            {
                Physics2D.IgnoreCollision(boxCollider2d, player.GetComponent<Collider2D>(), true);
            }
        }
    }
}
