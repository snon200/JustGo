using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField] private float jumpAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player")) // collided with the player
        {
            // add force to the player in the direction of the platform
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(jumpAmount * transform.up.x, jumpAmount * transform.up.y, jumpAmount * transform.up.z), ForceMode.VelocityChange);
        }
    }
}
