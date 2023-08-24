using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    // [SerializeField] private Transform[] spawnPoints;

    private float startScale=.2f;
    private float endScale=1f;

    private float time;

    public Vector3 playerPos;
    private Vector3 startPos;

    private float timer;

    void Start()
    {
        // Randomly picks sprite and spawn point
        this.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.position = new Vector3(Random.Range(-16f, 14f), -.1f, 0f);
        startPos = this.transform.position;
        // this.transform.position = spawnPoints[Random.Range(0,spawnPoints.Length)].position + new Vector3(((Time.time - EnemySpawner.startTime) * .2f) % 31,0f,0f);

        // Increases speed based on time since start of the game
        float timeSinceStart = Time.time - EnemySpawner.startTime;
        time = Vector3.Distance(playerPos, this.transform.position) / (timeSinceStart % 5 / 5 + .5f);
        timer = 0f;
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate() {
        timer += Time.fixedDeltaTime;
        this.transform.position = Vector3.Lerp(startPos, playerPos, timer / time);
        float temp = Mathf.Lerp(startScale, endScale, timer / time);
        this.transform.localScale = new Vector3(temp, temp, temp);

        if (timer > time)
        {
          playerHealth.inst.DamagePlayer(1);
          Destroy(this.gameObject);
        }
    }

    // When the envelope collides with itself, play animation, add score, and delete the envelope game object
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Envelope")
        {
            playerHealth.inst.AddScore();
            Destroy(this.gameObject);
            Destroy(col.gameObject);
        }
    }

}
