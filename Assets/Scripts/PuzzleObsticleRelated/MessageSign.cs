using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageSign : MonoBehaviour
{
    public Text MessageBox;
    public string Message;
    bool displayMessage;
    
    // Start is called before the first frame update
    void Start()
    {
        MessageBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(displayMessage)
        {
            Vector3 TextPos = Camera.main.WorldToScreenPoint(this.transform.position);
            MessageBox.transform.position = TextPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            MessageBox.enabled = true;
            MessageBox.text = Message;
            displayMessage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            MessageBox.enabled = false;
            displayMessage = false;
        }
    }
}
