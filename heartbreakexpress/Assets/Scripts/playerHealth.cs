using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public void Start()
    {
        hs.DrawHearts(hearts, maxHearts);
    }

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

    void Update()
    {
        playerScoreCount.text = playerScore.ToString();
    }
    
    public void AddScore()
    {
        playerScore += 1;
    }

    void Die()
    {
      Time.timeScale = 0f;
    }
}
