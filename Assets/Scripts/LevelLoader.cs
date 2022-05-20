using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    Animator transition;
    public bool isGameOver = false;
    public float transitionTime = 1f;

    private void Start()
    {
        transition = GetComponent<Animator>();
    }

    public void loadNextLevel()
    {
        //StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(loadLevel("Shop"));
    }
    IEnumerator loadLevel(string level)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(level);
    }
    
    void FixedUpdate(){
        if (isGameOver){
            StartCoroutine(loadEnding());
        }
    }
    IEnumerator loadEnding(){
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(loadLevel("Ending"));
    }
    public void restart(){
        SceneManager.LoadScene("Main Menu");
    }
}
