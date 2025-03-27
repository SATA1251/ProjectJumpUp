using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public ObjectPool objectPool;
    public float spawnInterval = 8.0f;

    public int minSpawn;
    public int maxSpawn;

    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    public float timer = 0;
    public int platformNumber = 0;

    private StageManager stageManager;

    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();


        switch (stageManager.GetStageNum())
        {
            case 1:
                spawnAreaMin = new Vector2(-2, -4);
                spawnAreaMax = new Vector2(2, 12);
                minSpawn = 1;
                maxSpawn = 3;
                break;
             case 2:
                spawnAreaMin = new Vector2(-2, -4);
                spawnAreaMax = new Vector2(2, 30);
                minSpawn = 1;
                maxSpawn = 5;
                break;
             case 3:
             break;
             case 4:
             break;
            default:
                break;
        }

    }

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
        for (int i = 0; i < platformCount; i++)
        {
            if (platformNumber < objectPool.poolSize)
            {
                float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
                float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
                Vector2 spawnPosition = new Vector2(randomX, randomY);

                GameObject platform = objectPool.GetObject();
                platform.GetComponent<RandomPlatform>().RespawnRandom();
                platform.GetComponent<RandomPlatform>().Respawn();
                platform.transform.position = spawnPosition;
                platformNumber++;
            }
        }
    }

    public void platformNumberDiscount()
    {
        platformNumber--;
    }
}
