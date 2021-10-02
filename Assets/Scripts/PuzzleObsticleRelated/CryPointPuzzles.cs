using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryPointPuzzles : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform objectDestination;
    private Vector3 OriginalPostion;
    public int speedOfMovement;
    EmotionManager player;
    bool Filled;
    bool Interactable;
    
    // Start is called before the first frame update
    void Start()
    {
        OriginalPostion = objectToMove.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Filled)
        {
            float step = speedOfMovement * Time.deltaTime;
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, objectDestination.position, step);
        }
        else if(!Filled)
        {
            float step = speedOfMovement * Time.deltaTime;
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, OriginalPostion, step);
        }


        if(Input.GetKeyDown(KeyCode.V) && Interactable)
        {
            if (player.playerEmotions == Emotions.SAD)
            {
                Filled = true;
            }
            else
            {
                Filled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<EmotionManager>();
            CanInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanInteract();
        }
    }

    void CanInteract()
    {
        Interactable = !Interactable;
    }
}
