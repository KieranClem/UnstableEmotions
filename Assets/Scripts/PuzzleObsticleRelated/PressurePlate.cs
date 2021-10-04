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

    IEnumerator CheckIfPlayerPassed()
    {
        Solved = ObjectToChange.SetIntangibility(true);

        yield return new WaitForSeconds(TimeLimit);

        Solved = ObjectToChange.SetIntangibility(false);
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
