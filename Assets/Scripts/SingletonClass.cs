using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonClass : MonoBehaviour
{
    public static SingletonClass instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this) //if duplicate instances
            Destroy(this);
        else
            instance = this;
    }
    
    [HideInInspector] public bool gamePaused = false;

    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload current scene

    public void ExitToMenu() => SceneManager.LoadScene("MainMenu");

    // set cursor properties
    public void SetCursor(bool enabled)
    {
        if (enabled)
            Cursor.lockState = CursorLockMode.None; // free cursor
        else
            Cursor.lockState = CursorLockMode.Locked; // lock cursor to middle of screen

        Cursor.visible = enabled; // make cursor visible/invisible
    }
}
