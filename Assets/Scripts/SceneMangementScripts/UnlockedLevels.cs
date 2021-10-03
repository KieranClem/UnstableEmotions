using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedLevels : MonoBehaviour
{
    public List<string> FinishedLevels;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("LevelTracker").Length > 1)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
