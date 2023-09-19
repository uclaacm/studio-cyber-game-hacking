using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script spawns the enemies at a constant spawnRate.
 */

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

    // Coroutine: https://docs.unity3d.com/ScriptReference/Coroutine.html
    // Basically a function that runs in parallel with other functions that is capable of being stopped and restarted (unlike Update() which is constantly running)
    // While canSpawn is true, the coroutine will wait a couple seconds (spawnRate) before instantiating an enemy object
    // It also randomizes the enemy's understanding of playerPos by adding noise to it
    // HACKABLE: Enemies spawn at a random location. Can you change this so they always spawn in the same place?
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
