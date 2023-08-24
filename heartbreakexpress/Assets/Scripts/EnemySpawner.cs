using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    public static float startTime;

    // all types of enemies
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private bool canSpawn = true; 
    [SerializeField] private Transform playerPos;

    private void Start() {
        startTime = Time.time;
        StartCoroutine(Spawner()); 
    } 

    private IEnumerator Spawner() {
        WaitForSeconds wait = new WaitForSeconds(spawnRate); 

        while (canSpawn) {
            yield return wait; 

            Vector3 noise = new Vector3(
                Random.Range(-.25f, .25f),
                Random.Range(-.25f, .25f),
                Random.Range(-.25f, .25f)
            );
            Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>().playerPos = playerPos.position + noise; 
        }
    }
}
