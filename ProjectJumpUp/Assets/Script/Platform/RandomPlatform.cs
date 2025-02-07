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
    // ���� ����� �����ͼ� ����
    public override void Disappear()
    {
        // gameObject.SetActive(false);  // �⺻������ 1�� �� ��Ȱ��ȭ
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
