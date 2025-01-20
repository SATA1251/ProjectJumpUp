using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatForm : MonoBehaviour
{

    public float lifeTime = 5.0f;
    public float collisionLifeTime = 3.0f;

    private float timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            ObjectPool pool = FindObjectOfType<ObjectPool>();
            pool.ReturnObject(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ReturnObjectDelay());
        }
    }

    private IEnumerator ReturnObjectDelay()
    {
        yield return new WaitForSeconds(collisionLifeTime);

        ObjectPool pool = FindObjectOfType<ObjectPool>();
        pool.ReturnObject(gameObject);
    }
}
