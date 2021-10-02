using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDetection : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EmotionManager>().playerEmotions != Emotions.SAD)
        {
            //Reset player to start of section
        }
    }
}
