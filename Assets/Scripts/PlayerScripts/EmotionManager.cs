using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotions
{
    NORMAL,
    HAPPY,
    SAD,
    ANGRY,
    EXCITED
}


public class EmotionManager : MonoBehaviour
{
    //Managing emotional state character is in
    [HideInInspector]public Emotions playerEmotions;

    //Stores playerMovement to adjust information as the character enters different emotional states
    private PlayerMovement playerMov;

    //Colours which represent the emotions
    private Color White = Color.white;
    private Color Green = Color.green;
    private Color Blue = Color.blue;
    private Color Red = Color.red;
    private Color Yellow = Color.yellow;
    private Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        playerMov = this.GetComponent<PlayerMovement>();
        playerEmotions = Emotions.NORMAL;
        rend = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Manages collision, be it things that change emotional state or other interactable objects reliant on emotional state
        switch (other.tag)
        {
            case "HappyTrigger":
                StopAllCoroutines();
                SetHappy();
                StartCoroutine(ResetToNormal(30));
                break;
            case "SadTrigger":
                StopAllCoroutines();
                SetSad();
                StartCoroutine(ResetToNormal(30));
                break;
            case "AngryTrigger":
                StopAllCoroutines();
                SetAngry();
                StartCoroutine(ResetToNormal(30));
                break;
            case "ExcitedTrigger":
                StopAllCoroutines();
                SetExcited();
                StartCoroutine(ResetToNormal(30));
                break;
            case "BreakableObject":
                if(playerEmotions == Emotions.ANGRY)
                {
                    DestroyGameObject(other.gameObject);
                }
                break;
            case "Enemy":
                if (playerEmotions == Emotions.ANGRY)
                {
                    DestroyGameObject(other.gameObject);
                }
                break;
        }
    }

    void DestroyGameObject(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);
    }

    //Set different emotional states
    void SetNormal()
    {
        playerEmotions = Emotions.NORMAL;
        rend.material.SetColor("_Color", White);
        playerMov.rb.drag = playerMov.normalPlayerDrag;
        playerMov.SlowFalling = false;
        playerMov.playerSpeed = playerMov.normalPlayerSpeed;
    }

    void SetHappy()
    {
        //SetNormal used to reset everything before changing to new emotion
        SetNormal();
        playerEmotions = Emotions.HAPPY;
        rend.material.SetColor("_Color", Green);
    }

    void SetSad()
    {
        //SetNormal used to reset everything before changing to new emotion
        SetNormal();
        playerEmotions = Emotions.SAD;
        rend.material.SetColor("_Color", Blue);
        playerMov.playerSpeed /= 2;
    }

    void SetAngry()
    {
        //SetNormal used to reset everything before changing to new emotion
        SetNormal();
        playerEmotions = Emotions.ANGRY;
        rend.material.SetColor("_Color", Red);
    }

    void SetExcited()
    {
        //SetNormal used to reset everything before changing to new emotion
        SetNormal();
        playerEmotions = Emotions.EXCITED;
        rend.material.SetColor("_Color", Yellow);
        playerMov.playerSpeed *= 2;
    }

    IEnumerator ResetToNormal(int effectTime)
    {
        
        yield return new WaitForSeconds(effectTime);
        SetNormal();
    }

}
