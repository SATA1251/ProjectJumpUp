using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlatform : MonoBehaviour, IPlatform
{
    // 모든 발판이 상속 받는 클래스
    protected bool isSteppendOn = false; // 중복 방지용 변수

    protected virtual float disappearTime => 3f;
    protected virtual float respawnTime => 5f;

    //public abstract void Initialize();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSteppendOn)
        {
            isSteppendOn = true;
            StartCoroutine(DisappearAfterDelay());
        }
    }
    
    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearTime);
        Disappear();
    }

    public virtual void Disappear()
    {
        gameObject.SetActive(false);  // 기본적으로 1초 후 비활성화
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
        gameObject.SetActive(true); 
    }
}
