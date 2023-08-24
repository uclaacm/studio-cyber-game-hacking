using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Unity.VisualScripting;
using Debug = System.Diagnostics.Debug;
public class UIScripts : MonoBehaviour
{

    [SerializeField] GameObject panel;
    //[SerializeField] GameObject pauseMenu;
    
    private GameObject[] spawners;
    
    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("Person"); // Finding the people that spawn
    }
    // Basic Scene Swapping Functionality
    public void navButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    // For when people no longer wish to shoot from a mail gun
    public void quitGame()
    {
        Application.Quit();
        UnityEngine.Debug.Log("Quit the application.");
    }
    // OPEN SESAME 
    public void openPanel()
    {
        if (panel != null)
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
        UnityEngine.Debug.Log("Panel.");
    }
    // ZA WARUDO!!!! TOKI WO TOMARE 
    public void pause()
    {
        //pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        foreach (GameObject spawner in spawners) // disable spawners to avoid dragging
        {
            spawner.SetActive(false);
        }
        UnityEngine.Debug.Log("pause");
    }
    // Time has begun to move again. 
    public void resume()
    {
        //pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        foreach (GameObject spawner in spawners) // disable spawners to avoid dragging
        {
            spawner.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
