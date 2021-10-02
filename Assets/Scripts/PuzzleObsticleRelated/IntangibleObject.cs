using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntangibleObject : MonoBehaviour
{
    private BoxCollider isSolid;
    private MeshRenderer objectMaterial;
    public bool playerPassed = false;
    public Material baseMaterial;
    public Material intangibleMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        isSolid = this.GetComponent<BoxCollider>();
        objectMaterial = this.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(isSolid.isTrigger == true)
            {
                playerPassed = true;
            }
        }    
    }

    public bool SetIntangibility()
    {
        if(isSolid.isTrigger == false)
        {
            isSolid.isTrigger = true;
            objectMaterial.material = intangibleMaterial;
            return false;
        }
        else
        {
            if(!playerPassed)
            {
                isSolid.isTrigger = false;
                objectMaterial.material = baseMaterial;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
