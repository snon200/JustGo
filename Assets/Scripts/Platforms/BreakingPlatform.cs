using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player")) // collided with the player
        {
            GetComponent<Animation>().Play(); // play breaking animation
        }
    }
}
