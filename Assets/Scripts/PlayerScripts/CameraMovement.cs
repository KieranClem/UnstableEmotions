using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Player;
    public float XDifference;
    public float YDifference;
    public float ZDifference;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Player.transform.position.x + XDifference, Player.transform.position.y + YDifference, Player.transform.position.z + ZDifference);
    }
}
