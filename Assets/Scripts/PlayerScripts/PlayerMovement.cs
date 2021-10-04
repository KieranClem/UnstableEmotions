using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //Basic player movement information
    public float playerSpeed;
    Vector3 Movement;
    bool isGrounded = true;
    public float jumpHeight = 5f;
    [HideInInspector] public Rigidbody rb;

    //Normal information of variables that change, used to reset variables after affect has happened
    [HideInInspector] public float normalPlayerDrag;
    [HideInInspector] public float normalPlayerSpeed;

    //Tracks what state the player is in
    EmotionManager playerEmotions;
    //stores if the player is floating or not
    [HideInInspector] public bool slowFalling = false;

    [HideInInspector] public Transform respawnPoint;

    public Animator playerAnimation;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        normalPlayerDrag = rb.drag;
        normalPlayerSpeed = playerSpeed;
        playerEmotions = this.GetComponent<EmotionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        playerAnimation.SetFloat("Speed", Mathf.Abs(Movement.y));

        if (Mathf.Abs(Movement.y) > 0.5f)
        {
            transform.position += Movement.y * Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized * playerSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Movement.y * Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized), 0.15f);
            playerAnimation.SetFloat("Speed",Mathf.Abs(Movement.y));
        }

        if(Mathf.Abs(Movement.x) > 0.5f)
        {
            transform.position += Movement.x * Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized * playerSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Movement.x * Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized), 0.15f);
            playerAnimation.SetFloat("Speed", Mathf.Abs(Movement.x));
        }

        if (isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                isGrounded = false;
            }
        }
    }

    //check if the player is falling last
    private void LateUpdate()
    {
        //Only activates if the player isn't grounded and is happy
        if (!isGrounded && (playerEmotions.playerEmotions == Emotions.HAPPY))
        {
            if (rb.velocity.y < -0.5)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    ActivateSlowFall(slowFalling);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor")
        {
            CancelInvoke("OffFloor");
            isGrounded = true;
            rb.drag = normalPlayerDrag;
            slowFalling = false;
        }

        if(other.tag == "RespawnPoint")
        {
            respawnPoint = other.transform;
            other.enabled = false;
            playerEmotions.DestoryPassedBrokenObjects();
            playerEmotions.DestroyPassedEnemies();
        }

        if(other.tag == "BottomlessPit")
        {
            ResetPosition();
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "BreakableObject")
        {
            if (playerEmotions.playerEmotions != Emotions.ANGRY)
            {
                isGrounded = true;
            }
        }

        if (other.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            Invoke("OffFloor", 0.5f);
        }
    }

    void OffFloor()
    {
        isGrounded = false;
    }

    void ActivateSlowFall(bool AlreadyActive)
    {
        //Increases drag to give the effect of floating
        if(!AlreadyActive)
        {
            rb.drag += 3;
            slowFalling = true;
        }
        else
        {
            rb.drag = normalPlayerDrag;
            slowFalling = false;
        }
    }

    public void ResetPosition()
    {
        playerEmotions.ResetBrokenObjects();
        playerEmotions.ResetDefeatedEnemies();
        this.transform.position = respawnPoint.position;
        playerEmotions.SetNormal();
    }
}
