using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelSelect : MonoBehaviour
{
    UnlockedLevels unlockedLevels;

    private void Start()
    {
        unlockedLevels = GameObject.FindGameObjectWithTag("LevelTracker").GetComponent<UnlockedLevels>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!unlockedLevels.FinishedLevels.Contains(SceneManager.GetActiveScene().name))
            {
                unlockedLevels.FinishedLevels.Add(SceneManager.GetActiveScene().name);
            }
            SceneManager.LoadScene("LevelSelect");
        }
    }
}
