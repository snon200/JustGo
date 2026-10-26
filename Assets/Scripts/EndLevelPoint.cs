using UnityEngine;

public class EndLevelPoint : MonoBehaviour
{
    [SerializeField] private bool levelComplete; // if point completes the level
    [SerializeField] private GameObject endLevelMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) // if player entered collider
        {
            if (levelComplete)
                endLevelMenu.SetActive(true); // enable end level canvas
            else
                SingletonClass.instance.RestartLevel(); // restart the current level
        }
    }
}
