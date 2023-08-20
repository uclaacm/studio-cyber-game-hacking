using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public bool paused;
    public Animator pauseMenu;

    void Start(){
        paused=false;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(paused)
                Resume();
            else
                Pause();
        }
    }
    void Pause(){
        paused=true;
        Time.timeScale=0f;
        pauseMenu.SetTrigger("Enter");
    }
    public void Resume(){
        paused=false;
        Time.timeScale=1f;
        pauseMenu.SetTrigger("Resume");
    }
    public void QuitTOMain(){
        SceneManager.LoadScene(0);
    }
}
