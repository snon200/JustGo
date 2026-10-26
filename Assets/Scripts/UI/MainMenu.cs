using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string firstLevelName; // name of the first scene
    [SerializeField] private TMP_InputField nameInputField;

    private void Start()
    {
        if (!PlayerPrefs.GetString("name", "").Equals("")) // if already set name
            nameInputField.text = PlayerPrefs.GetString("name"); // load player name
    }

    public void StartGame()
    {
        LoadLevel(firstLevelName);
    }

    public void LoadLevel(string levelName) 
    {
        if (!nameInputField.text.Equals("")) // if name not empty
        {
            PlayerPrefs.SetString("name", nameInputField.text); // set player name

            SceneManager.LoadScene(levelName, LoadSceneMode.Single); // load selected level
        }
        else
        {
            Debug.Log("Empty user name");
        }
    }

    public void ExitGame() => Application.Quit();
}
