using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlatform : MonoBehaviour, IPlatform
{
    // ��� ������ ��� �޴� Ŭ����
    protected bool isSteppendOn = false; // �ߺ� ������ ����

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
        gameObject.SetActive(false);  // �⺻������ 1�� �� ��Ȱ��ȭ
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
