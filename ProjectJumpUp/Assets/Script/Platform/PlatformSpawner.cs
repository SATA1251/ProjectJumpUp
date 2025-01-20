using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public ObjectPool objectPool;
    public float spawnInterval = 2.0f;

    public int minSpawn = 1;
    public int maxSpawn = 5;

    public Vector2 spawnAreaMin = new Vector2(-2, -4);
    public Vector2 spawnAreaMax = new Vector2(2, 3);

    public float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            // 여기에 스폰
            SpawnPlatform();
            timer = 0;
        }
    }

    void SpawnPlatform()
    {
        int platformCount = Random.Range(minSpawn, maxSpawn + 1);

        for(int i = 0; i < platformCount; i++)
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            GameObject platform = objectPool.GetObject();
            platform.transform.position = spawnPosition;
        }
    }
}
