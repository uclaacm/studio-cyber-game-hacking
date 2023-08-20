using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [Header ("Enemies")]
    public GameObject square;
    public GameObject triangle;
    public GameManager game;
    [Header ("Spawning Details")]
    public bool paused;
    public float rate;
    private float spawnTimer;
    
    public float maxHeight;
    public float minHeight;


    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        Random.InitState(7019542);
        spawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer+=Time.deltaTime;
        if(spawnTimer >= rate){
            spawnWave();
            spawnTimer=0;
        }
    }

    void spawnWave(){
        //pick 1-4 locations 
        int numEnemies = Random.Range(0,5)+1;
        Debug.Log(numEnemies);
        float stagger = Mathf.Abs(maxHeight-minHeight)/(numEnemies+1);


        for (int x=1;x<=numEnemies;x++){
            Vector3 spawnPosition = new Vector3(transform.position.x,transform.position.y+maxHeight-stagger*x,transform.position.z);
            Quaternion angle = Quaternion.identity;
            //generate random enemy
            int number = Random.Range(0,2);
            GameObject enemy = null;
            switch(number){
                case 0:
                    enemy= triangle;
                    break;
                case 1:
                    enemy= square;
                    break;
            }

            if(number==0)
                angle = Quaternion.Euler(0,0,90);

            HealthStat stat = Instantiate(enemy,spawnPosition,angle,this.gameObject.transform).GetComponent<HealthStat>();
            stat.OnUnitKilled += addPoints;
        }
    }

    void addPoints(){
        game.addPoints();
    }


}
