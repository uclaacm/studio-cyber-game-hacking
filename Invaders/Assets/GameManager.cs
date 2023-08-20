using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public HealthStat targetHealth1;
    public HealthStat targetHealth2;
    public Animator animator;

    [Header ("Score")]
    public int score;
    public TextMeshProUGUI text;
    public TextMeshProUGUI in_game_text;
    // Start is called before the first frame update
    void Start()
    {

        score=0;
        targetHealth1.OnUnitKilled += GameOver;
        targetHealth2.OnUnitKilled += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver(){
        
        animator.SetTrigger("Game Over");

    }

    public void Replay(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void addPoints(){
        score+=100;
        text.text ="Score: "+score;
        in_game_text.text = "Score: "+score;
    }

}
