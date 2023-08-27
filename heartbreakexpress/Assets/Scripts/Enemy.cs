using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to each enemy object and controls its appearance, spawn position, and movement speed. 
 * It damages the player when it reaches the player and increments the score when destroyed by an envelope. 
 * The script playerHealth determines player health and score. 
 */

public class Enemy : MonoBehaviour 
{
    [SerializeField] private Sprite[] sprites;

    private float startScale=.2f;
    private float endScale=1f;

    private float time;

    public Vector3 playerPos;
    private Vector3 startPos;

    private float timer;

    void Start()
    {
        // Randomly picks an enemy sprite from a list of sprites and a random spawn location given a range
        this.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.position = new Vector3(Random.Range(-16f, 14f), -.1f, 0f);
        startPos = this.transform.position;

        // Increases enemy move speed based on time since start of the game
        float timeSinceStart = Time.time - EnemySpawner.startTime;
        time = Vector3.Distance(playerPos, this.transform.position) / (timeSinceStart % 5 / 5 + .5f);
        timer = 0f;
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate() {
        // each frame, moves the enemy towards playerPos 
        // Vector3.Lerp: https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        timer += Time.fixedDeltaTime;
        this.transform.position = Vector3.Lerp(startPos, playerPos, timer / time);
        
        // each frame, makes the enemy larger as it gets closer to playerPos
        float temp = Mathf.Lerp(startScale, endScale, timer / time);
        this.transform.localScale = new Vector3(temp, temp, temp);

        // checks if the enemy has reached player position
        // if the enemy has, damage the player and destroy the enemy object
        if (timer > time)
        {
          playerHealth.inst.DamagePlayer(1);
          Destroy(this.gameObject);
        }
    }

    // When the envelope collides with the enemy, play animation, add score, and delete the envelope game object
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Envelope")
        {
            playerHealth.inst.AddScore();
            Destroy(this.gameObject);
            Destroy(col.gameObject);
        }
    }

}
