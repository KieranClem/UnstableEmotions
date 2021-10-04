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

    public void LoadLevelThree()
    {
        SceneManager.LoadScene("Level3");
    }

    public void LoadLevelFour()
    {
        SceneManager.LoadScene("Level4");
    }

    public void LoadLevelFive()
    {
        SceneManager.LoadScene("Level5");
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
