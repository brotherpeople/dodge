using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [HideInInspector]
    public bool isGameover = false;
    
    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI bulletCountText;

    private float playTime = 0f;

    // singleton
    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public void IncreaseTime() {
        if (!isGameover)
        {
            playTime += Time.deltaTime;
            timeText.SetText(playTime.ToString("F2"));
        }

    }

    public float GetPlayTime() {
        return playTime;
    }
    public void IncreaseBulletCount() {
        bulletCountText.SetText(Bullet.bulletCount.ToString());
    }

    public void SetGameOver() {
        isGameover = true;
        BulletSpawner bulletSpawner = FindObjectOfType<BulletSpawner>();

        if (bulletSpawner != null) {
            bulletSpawner.StopEnemyRoutine();
        }
    }
}
