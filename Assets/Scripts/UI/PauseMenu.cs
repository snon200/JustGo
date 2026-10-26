using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) // if player pressed escape button
        {
            // toggle pause menu
            SingletonClass.instance.gamePaused = !transform.GetChild(0).gameObject.activeSelf;
            transform.GetChild(0).gameObject.SetActive(SingletonClass.instance.gamePaused);

            SingletonClass.instance.SetCursor(SingletonClass.instance.gamePaused); // toggle cursor
        }
    }

    public void RestartLevel() => SingletonClass.instance.RestartLevel();

    public void ExitToMenu() => SingletonClass.instance.ExitToMenu();
}
