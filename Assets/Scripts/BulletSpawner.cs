using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject Bullet;
    private UnityEngine.Vector3 direction = UnityEngine.Vector3.zero;

    // set origin location outside the box
    // 1. from leftside: -5.6f < posX < -4.6f, -5.6f < posY < 5.6f
    // 2. from rightside: 4.6f < posX < 5.6f, -5.6f < posY < 5.6f
    // 3. from upside: -5.6f < posX < 5.6f, 4.6f < posY < 5.6f
    // 4. from downside: -5.6f < posX < 5.6f, -5.6f < posY < -4.6f
    
    // towards anywhere but outside the box
    // 1. from leftside: -4.6f < posX, 
    void Start() { StartEnemyRoutine(); }
    void StartEnemyRoutine() { StartCoroutine("BulletRoutine"); }
    public void StopEnemyRoutine() { StopCoroutine("BulletRoutine"); }

    IEnumerator BulletRoutine() {
        yield return new WaitForSeconds(1f);
        direction = Random.insideUnitCircle.normalized;

    }
    void SpawnBullet(float posInitX, float posInitY, UnityEngine.Vector3 direction) {
        posInitX = Random.Range(-5.6f, -4.6f);
        posInitY = Random.Range(-5.6f, 5.6f);
    }
}
