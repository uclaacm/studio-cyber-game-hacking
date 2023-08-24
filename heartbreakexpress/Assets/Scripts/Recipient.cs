using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Recipient : MonoBehaviour {
    
    public float playerScore;
    public TMP_Text playerScoreCount;
    
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    // When clicked on, IDK why this is here
    // void OnMouseDown() {
    //     Debug.Log("Get Destroyed");
    // }

    // When the envelope collides with itself, play animation, add score, and delete the envelope game object
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(col.gameObject.name);
        Debug.Log("Get Destroyed");
        AddScore();
        Destroy(col.gameObject);
        // Play animation
    }
    void AddScore()
    {
        playerScore += 1f;
        playerScoreCount.text = playerScore.ToString();
    }
}
