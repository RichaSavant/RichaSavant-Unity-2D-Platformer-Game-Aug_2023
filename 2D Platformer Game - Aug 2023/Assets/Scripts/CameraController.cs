using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//We create a separate script for the camera to ensure it does not follow the player on the z axis.

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    //Don't need Start method.
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
