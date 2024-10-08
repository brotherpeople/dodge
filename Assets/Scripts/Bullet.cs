using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public static int bulletCount = 0;
    public float speed;
    private GameManager gameManager;

    void Start()
    {
        bulletCount++;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = Random.Range(1, 3);

        if(!gameManager.isGameover) {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (transform.position.x > 5f || transform.position.x < -5f || transform.position.y > 5f || transform.position.y < -5f)
        {
            Destroy(gameObject);
            bulletCount--;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") {
            speed = 0;
            gameManager.SetGameOver();
        }
    }

}
