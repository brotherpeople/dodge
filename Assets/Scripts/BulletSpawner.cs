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

    public float spawnInterval = 1f;
    private float timeSinceLastSpawn = 0f;

    void Start()
    {
        StartEnemyRoutine();
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
        }

        while (true)
        {
            if (timeSinceLastSpawn >= spawnInterval && Bullet.bulletCount < maxBullets)
            {
                SpawnBullet();
                timeSinceLastSpawn = 0f;
            }
            yield return new WaitForSeconds(spawnInterval);
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
                spawnPosition = new UnityEngine.Vector2(Random.Range(-4.6f, 4.6f), 4.6f);
                UnityEngine.Vector2 toDown = new UnityEngine.Vector2(Random.Range(-5f, 5f), -5f);
                direction = (toDown - spawnPosition).normalized;
                break;
            case 3: // from down
                spawnPosition = new UnityEngine.Vector2(Random.Range(-4.6f, 4.6f), -4.6f);
                UnityEngine.Vector2 toAbove = new UnityEngine.Vector2(Random.Range(-5f, 5f), 5f);
                direction = (toAbove - spawnPosition).normalized;
                break;
        }

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, UnityEngine.Quaternion.identity);
        bullet.GetComponent<Bullet>().direction = direction;
    }
}
