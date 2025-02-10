using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;

    [SerializeField]
    private int initialBullets = 20;

    [SerializeField]
    private int maxBullets = 30;

    private float lastBulletIncrease = 0f;


    void Start()
    {
        StartEnemyRoutine();
    }

    void Update() {
        float currentTime = GameManager.instance.GetPlayTime();
        if (currentTime - lastBulletIncrease >= 5f && !GameManager.instance.isGameover)
        {
            maxBullets++;
            lastBulletIncrease = currentTime;
        }
    }

    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine()
    {
        for (int i = 0; i < initialBullets; i++)
        {
            SpawnBullet();
            yield return new WaitForSeconds(0.1f);
        }

        while (true)
        {
            if (Bullet.bulletCount < maxBullets)
            {
                int bulletToSpawn = maxBullets - Bullet.bulletCount;
                for (int i = 0; i < bulletToSpawn; i++)
                {
                    SpawnBullet();
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void SpawnBullet()
    {
        int randomSide = Random.Range(0, 4);
        UnityEngine.Vector2 spawnPosition = UnityEngine.Vector2.zero;
        UnityEngine.Vector2 direction = UnityEngine.Vector2.zero;

        switch (randomSide)
        {
            case 0: // from left
                spawnPosition = new UnityEngine.Vector2(-5f, Random.Range(-5f, 5f));
                UnityEngine.Vector2 toRight = new UnityEngine.Vector2(5f, Random.Range(-5f, 5f));
                direction = (toRight - spawnPosition).normalized;
                break;
            case 1: // from right
                spawnPosition = new UnityEngine.Vector2(5f, Random.Range(-5f, 5f));
                UnityEngine.Vector2 toLeft = new UnityEngine.Vector2(-5f, Random.Range(-5f, 5f));
                direction = (toLeft - spawnPosition).normalized;
                break;
            case 2: // from above
                spawnPosition = new UnityEngine.Vector2(Random.Range(-5f, 5f), 5f);
                UnityEngine.Vector2 toDown = new UnityEngine.Vector2(Random.Range(-5f, 5f), -5f);
                direction = (toDown - spawnPosition).normalized;
                break;
            case 3: // from down
                spawnPosition = new UnityEngine.Vector2(Random.Range(-5f, 5f), -5f);
                UnityEngine.Vector2 toAbove = new UnityEngine.Vector2(Random.Range(-5f, 5f), 5f);
                direction = (toAbove - spawnPosition).normalized;
                break;
        }

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, UnityEngine.Quaternion.identity);
        bullet.GetComponent<Bullet>().direction = direction;
    }
}
