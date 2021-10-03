using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public void LoadTutorialLevel()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }
}
