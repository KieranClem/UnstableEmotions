using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Different emotions the player character can be under
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

    public int effectTimer = 15;

    //Colours which represent the emotions
    private Color Gray = Color.gray;
    private Color Green = Color.green;
    private Color Blue = Color.blue;
    private Color Red = Color.red;
    private Color Yellow = Color.yellow;
    private Renderer rend;

    List<MeshRenderer> BrokenObjects = new List<MeshRenderer>();
    List<EnemyAI> defeatedEnemies = new List<EnemyAI>();

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
                StartCoroutine(ResetToNormal(effectTimer));
                break;
            case "SadTrigger":
                StopAllCoroutines();
                SetSad();
                StartCoroutine(ResetToNormal(effectTimer));
                break;
            case "AngryTrigger":
                StopAllCoroutines();
                SetAngry();
                StartCoroutine(ResetToNormal(effectTimer));
                break;
            case "ExcitedTrigger":
                StopAllCoroutines();
                SetExcited();
                StartCoroutine(ResetToNormal(effectTimer));
                break;
            case "BreakableObject":
                if(playerEmotions == Emotions.ANGRY)
                {
                    BrokenObjects.Add(other.GetComponent<MeshRenderer>());
                    other.GetComponent<MeshRenderer>().enabled = false;
                    other.GetComponent<BoxCollider>().enabled = false;
                }
                break;
            case "Enemy":
                if (playerEmotions == Emotions.ANGRY)
                {
                    defeatedEnemies.Add(other.GetComponent<EnemyAI>());
                    other.GetComponent<MeshRenderer>().enabled = false;
                    other.GetComponent<BoxCollider>().enabled = false;
                    other.GetComponent<EnemyAI>().StopEnemy();
                }
                else
                {
                    StopAllCoroutines();
                    playerMov.ResetPosition();
                }
                break;
        }
    }

    public void ResetBrokenObjects()
    {
        if (BrokenObjects.Count != 0)
        {
            foreach (MeshRenderer renderer in BrokenObjects)
            {
                renderer.enabled = true;
                renderer.GetComponent<BoxCollider>().enabled = true;
                BrokenObjects.Remove(renderer);
            }
            BrokenObjects = new List<MeshRenderer>();
        }
    }

    public void DestoryPassedBrokenObjects()
    {
        if (BrokenObjects.Count != 0)
        {
            foreach (MeshRenderer renderer in BrokenObjects)
            {
                Destroy(renderer.gameObject);
            }
            BrokenObjects = new List<MeshRenderer>();
        }
    }

    public void ResetDefeatedEnemies()
    {
        if(defeatedEnemies.Count != 0)
        {
            foreach(EnemyAI enemy in defeatedEnemies)
            {
                enemy.StartMoving();
                enemy.GetComponent<MeshRenderer>().enabled = true;
                enemy.GetComponent<BoxCollider>().enabled = true;
                defeatedEnemies.Remove(enemy);
            }
            defeatedEnemies = new List<EnemyAI>();
        }
    }

    public void DestroyPassedEnemies()
    {
        if(defeatedEnemies.Count != 0)
        {
            foreach(EnemyAI enemy in defeatedEnemies)
            {
                Destroy(enemy.gameObject);
            }
            defeatedEnemies = new List<EnemyAI>();
        }
    }

    void DestroyGameObject(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);
    }

    //Set different emotional states
    public void SetNormal()
    {
        playerEmotions = Emotions.NORMAL;
        rend.material.SetColor("_Color", Gray);
        playerMov.rb.drag = playerMov.normalPlayerDrag;
        playerMov.slowFalling = false;
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
