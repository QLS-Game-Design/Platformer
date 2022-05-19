using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    public GameObject levelLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StaticTracker.UpdateCoins();
            levelLoader.GetComponent<LevelLoader>().loadNextLevel();
        }
    }
}
