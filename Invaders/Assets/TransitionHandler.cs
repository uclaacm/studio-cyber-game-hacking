using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionHandler : MonoBehaviour
{

    private int nextSceneIndex;
    private Animator animator;
    public void Start(){
        animator = GetComponent<Animator>();
    }

    public void FadeToScene(int m){
        animator.SetTrigger("FadeOut");
        nextSceneIndex=m;
    }

    public void loadScene(){
        SceneManager.LoadScene(nextSceneIndex);
    }

}
