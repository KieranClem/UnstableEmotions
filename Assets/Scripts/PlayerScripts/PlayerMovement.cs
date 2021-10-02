﻿using System.Collections;
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
    [HideInInspector] public bool SlowFalling = false;

    
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

        if(Mathf.Abs(Movement.y) > 0.5f)
        {
            transform.position += Movement.y * Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized * playerSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Movement.y * Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized), 0.15f);
        }

        if(Mathf.Abs(Movement.x) > 0.5f)
        {
            transform.position += Movement.x * Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized * playerSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Movement.x * Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized), 0.15f);
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
            if (rb.velocity.y < 0)
            {
                if(Input.GetKeyDown(KeyCode.V))
                {
                    ActivateSlowFall(SlowFalling);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor")
        {
            isGrounded = true;
            rb.drag = normalPlayerDrag;
            SlowFalling = false;
        }
    }

    void ActivateSlowFall(bool AlreadyActive)
    {
        //Increases drag to give the effect of floating
        if(!AlreadyActive)
        {
            rb.drag += 3;
            SlowFalling = true;
        }
        else
        {
            rb.drag = normalPlayerDrag;
            SlowFalling = false;
        }
    }
}
