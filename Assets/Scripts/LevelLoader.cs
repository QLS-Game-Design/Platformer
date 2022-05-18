using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    Animator transition;
    public bool isGameOver = false;
    public float transitionTime = 1f;

    public void loadNextLevel()
    {
        transition = GetComponent<Animator>();
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator loadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
    
    void FixedUpdate(){
        if (isGameOver){
            StartCoroutine(loadEnding());
        }
    }
    IEnumerator loadEnding(){
        yield return new WaitForSeconds(1.5f);
        loadNextLevel();
    }
}
