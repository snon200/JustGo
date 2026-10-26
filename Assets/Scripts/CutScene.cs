using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    [SerializeField] private string nextLevelName;

    private void OnEnable() => SceneManager.LoadScene(nextLevelName); // load next level
}
