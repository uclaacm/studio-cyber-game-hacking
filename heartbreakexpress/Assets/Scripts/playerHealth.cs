using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

/*
 * This script is used to determine player health and player score as well as calling HealthSystem to draw the lives onto the screen. 
 */

public class playerHealth : MonoBehaviour
{
    public static playerHealth inst;

    //HACKABLE: Look into increasing maxHearts. Is it possible to never lose health
    public int hearts = 3;
    public int Hearts => hearts;
    public int maxHearts = 3;
    public int MaxHearts => maxHearts;

    [SerializeField] public HealthSystem hs;
    [SerializeField] private TMP_Text playerScoreCount;
    private int playerScore;
    public int PlayerScore => playerScore;

    private int dmg = 1;
    public int Dmg => dmg;

    private int points = 1;
    public int Points => points;

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
    public void DamagePlayer()
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
    // HACKABLE: Where is this function called? Can you change its behavior or call it more often?
    public void AddScore()
    {
        playerScore += points;
    }

    // when you die, load the main menu 
    void Die()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
