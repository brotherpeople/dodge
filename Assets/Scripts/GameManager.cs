using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isGameover = false;

    public void SetGameOver() {
        isGameover = true;
        BulletSpawner bulletSpawner = FindObjectOfType<BulletSpawner>();

        if (bulletSpawner != null) {
            bulletSpawner.StopEnemyRoutine();
        }
    }
}
