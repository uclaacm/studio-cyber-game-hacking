using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{

    public void Start(){
        Time.timeScale=1f;
    }
    public void quit(){
        Application.Quit();
    }
    public void Play(){
        SceneManager.LoadScene(1);
    }
    public void onVolumeChange(Slider value){
        AudioListener.volume = value.value;
    }
}
