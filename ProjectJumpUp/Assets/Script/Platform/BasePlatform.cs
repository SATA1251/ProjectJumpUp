using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlatform : MonoBehaviour, IPlatform
{
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D boxCollider2d;
    // 모든 발판이 상속 받는 클래스
    protected bool isSteppendOn = false; // 중복 방지용 변수

    protected virtual float disappearTime => 2f;
    protected virtual float respawnTime => 5f;

    protected PlayerController playerController;

    protected bool isOnPlayer = false;

    private void Start()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isSteppendOn)
        {
            if(playerController.isOnPlatform == false)
            {
                isOnPlayer = true;
            }

            if(isOnPlayer == true)
            {
                isSteppendOn = true;
                StartCoroutine(DisappearAfterDelay());
            }
        }
    }
    
    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearTime);
        Disappear();
    }

    public virtual void Disappear()
    {
        //gameObject.SetActive(false);  // 기본적으로 1초 후 비활성화

        if(spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        if(boxCollider2d != null)
        {
            boxCollider2d.enabled = false;
        }

        StartCoroutine(RespawnAfterDelay());
    }

    protected virtual IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);
        Respawn();
    }

    public virtual void Respawn()
    {
        isSteppendOn = false;
        isOnPlayer = false;
        // gameObject.SetActive(true); 
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
