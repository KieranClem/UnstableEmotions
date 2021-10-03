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
}
