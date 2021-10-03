using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageUnlockedLevels : MonoBehaviour
{
    public List<Button> LevelButtons;
    public List<string> FinishedLevels;
    
    // Start is called before the first frame update
    void Start()
    {
        FinishedLevels = GameObject.FindGameObjectWithTag("LevelTracker").GetComponent<UnlockedLevels>().FinishedLevels;
        
        LevelButtons[0].interactable = true;
        
        for(int i = 0; i <= FinishedLevels.Count; i++)
        {
            LevelButtons[i].interactable = true;
            if(i != FinishedLevels.Count)
            {
                LevelButtons[i].image.color = Color.green;
            }
            else
            {
                LevelButtons[i].image.color = Color.yellow;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
