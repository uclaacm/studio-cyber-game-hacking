using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This script is used to determine player health and player score as well as calling HealthSystem to draw the lives onto the screen. 
 */

public class playerHealth : MonoBehaviour
{
    public static playerHealth inst;

    public int hearts = 3;
    public int maxHearts = 3;
    [SerializeField] public HealthSystem hs;
    [SerializeField] private TMP_Text playerScoreCount;
    private int playerScore;

    void Awake()
    {
      if (!inst) inst = this;
      else Destroy(this.gameObject);
    }

    // draw the envelopes for the number of lives player has when game starts
    public void Start()
    {
        hs.DrawHearts(hearts, maxHearts);
    }

    // function for damaging the player and dying when the player has no more lives
    // redraws envelopes for each life lost
    public void DamagePlayer(int dmg)
    {
        if (hearts > 0)
        {
            hearts -= dmg;
            hs.DrawHearts(hearts, maxHearts);
            if (hearts <= 0)
            {
              Die();
            }
        }
    }

    // writes the player score 
    void Update()
    {
        playerScoreCount.text = playerScore.ToString();
    }
    
    // adds +1 to player score
    public void AddScore()
    {
        playerScore += 1;
    }

    // when you die, time stops 
    void Die()
    {
      Time.timeScale = 0f;
    }
}
