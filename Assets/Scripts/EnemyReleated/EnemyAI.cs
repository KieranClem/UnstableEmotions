using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent nav;
    private GameObject player;
    int travelPointer = 0;
    public int chaseTimer = 5;
    public List<Transform> travelPos;
    RaycastHit hit;
    bool chasing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();
        SetNextDestination();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!nav.pathPending)
        {
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                if (nav.hasPath || nav.velocity.sqrMagnitude == 0f && chasing == false)
                {
                    SetNextDestination();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, 15))
        {
            if (hit.collider.tag == "Player")
            {
                nav.SetDestination(hit.transform.position);
                chasing = true;
                player = hit.transform.gameObject;
                StartCoroutine(Counting());
            }
        }

        if (chasing)
            nav.SetDestination(player.transform.position);
    }

    void SetNextDestination()
    {
        nav.SetDestination(travelPos[travelPointer].position);
        travelPointer += 1;
        if(travelPointer >= travelPos.Count)
        {
            travelPointer = 0;
        }
    }

    IEnumerator Counting()
    {

        yield return new WaitForSeconds(chaseTimer);
        chasing = false;
        travelPointer = 0;
        SetNextDestination();
    }
}
