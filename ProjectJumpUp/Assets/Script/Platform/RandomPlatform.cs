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
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
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


    // ���� ����� �����ͼ� ����
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
        Debug.Log("�����÷������� ��Ȱ��ȭ ����");
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
    }
}
