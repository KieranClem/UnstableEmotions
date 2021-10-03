using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public IntangibleObject ObjectToChange;
    public float TimeLimit;
    bool Solved = false;

    
    // Start is called before the first frame update
    void Start()
    {
        Solved = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckIfPlayerPassed()
    {
        Solved = ObjectToChange.SetIntangibility();

        yield return new WaitForSeconds(TimeLimit);

        Solved = ObjectToChange.SetIntangibility();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!Solved)
            {
                StopAllCoroutines();
                StartCoroutine(CheckIfPlayerPassed());
            }
        }
    }
}
