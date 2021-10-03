using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRight : MonoBehaviour
{
    private Transform Movement;
    private Vector3 firstPosition;
    private Vector3 secondPosition;
    private Vector3 targetPosition;
    public float movementDistance;
    public float speed;
    bool movLeftRight;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Movement = this.transform;
        firstPosition = new Vector3(Movement.position.x, Movement.position.y, Movement.position.z + movementDistance);
        secondPosition = new Vector3(Movement.position.x , Movement.position.y, Movement.position.z - movementDistance);
        targetPosition = firstPosition;
        movLeftRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Round(Movement.position.z) == Mathf.Round(targetPosition.z))
        {
            if(movLeftRight)
            {
                targetPosition = secondPosition;
                movLeftRight = false;
            }
            else
            {
                targetPosition = firstPosition;
                movLeftRight = true;
            }
        }

        Movement.position = Vector3.MoveTowards(Movement.position, targetPosition, speed * Time.deltaTime);
    }
}
