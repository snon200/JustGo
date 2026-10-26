using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullZone : MonoBehaviour
{
    private bool hitPlayer = false;
    private Transform playerTransform;
    
    private void FixedUpdate()
    {
        if (hitPlayer && !SingletonClass.instance.gamePaused) // player is inside pull zone and game not paused
            playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 0.5f, playerTransform.position.z); // lower the player position
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Player")) // collided with the player
        {
            hitPlayer = true;
            playerTransform = collider.transform;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag.Equals("Player")) // collided with the player
            hitPlayer = false;
    }
}
