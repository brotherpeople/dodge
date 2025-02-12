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

    [SerializeField]
    private TextMeshProUGUI scoreTextInGame;

    [SerializeField]
    private TextMeshProUGUI scoreTextGameOver;

    [SerializeField]
    private GameObject startPanel;

    [SerializeField]
    private GameObject gameOverPanel;

    private int score = 0;

    public bool isGameStarted = false;

    private float playTime = 0f;

    // singleton
    void Start() {
        Time.timeScale = 0f;
        startPanel.SetActive(true);
    }

    void Awake()
    {
        if(instance == null) {
            instance = this;
        }
    }

    void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space))  
        {
            StartGame();
        }
        else if (isGameover && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        isGameover = false;
        playTime = 0f;
        score = 0;

        gameOverPanel.SetActive(false);
        // startPanel.SetActive(false);
        scoreTextInGame.SetText("0");
        scoreTextGameOver.SetText("0");
        timeText.SetText("0.00");
        bulletCountText.SetText("0");

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        Bullet.bulletCount = 0;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = Vector3.zero;
            player.GetComponent<Player>().enabled = true;
        }

        Time.timeScale = 1f;

        BulletSpawner bulletSpawner = FindObjectOfType<BulletSpawner>();
        if (bulletSpawner != null)
        {
            bulletSpawner.StartEnemyRoutine();
        }

    }

    void StartGame() 
    {
        isGameStarted = true;
        Time.timeScale = 1f;
        startPanel.SetActive(false);

        BulletSpawner bulletSpawner = FindObjectOfType<BulletSpawner>();
        if (bulletSpawner != null)
        {
            bulletSpawner.StartEnemyRoutine();
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

    public void setScore() {
        if (isGameover || BulletSpawner.instance == null) return;
        
        score = (int)(GetPlayTime() * BulletSpawner.instance.GetMaxBullets() * (1 + GetPlayTime() / 60f));
        scoreTextInGame.SetText(score.ToString());
        scoreTextGameOver.SetText(score.ToString());
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

        Invoke("ShowGameOverPanel", 0.1f);
    }

    void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }

}
