using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        float newXpos = transform.position.x + horizontalInput * moveSpeed * Time.deltaTime;
        float newYpos = transform.position.y + verticalInput * moveSpeed * Time.deltaTime;
        float toX = Mathf.Clamp(newXpos, -4.6f, 4.6f);
        float toY = Mathf.Clamp(newYpos, -4.6f, 4.6f);

        transform.position = new Vector3(toX, toY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet") {
            Debug.Log("Game Over");
            gameObject.GetComponent<Player>().enabled = false;
        }
    }
}
