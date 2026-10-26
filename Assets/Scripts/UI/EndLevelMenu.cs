using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Dan.Main;

public class EndLevelMenu : MonoBehaviour
{
    [SerializeField] private string leaderboardKey;
    [SerializeField] private string nextLevelName;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI leaderboardText;

    private LeaderboardReference leaderboardReference;

    private int myScore = 999;
    private int minScore = 999;

    private bool uploaded = false;

    // called when objected is enabled
    private void OnEnable()
    {
        SingletonClass.instance.gamePaused = true; // pause game

        SingletonClass.instance.SetCursor(true); // enable cursor

        // set completion time text
        Timer timer = GameObject.FindGameObjectWithTag("Player").GetComponent<Timer>();
        myScore = timer.time;
        timeText.text = $"{(myScore / 60).ToString("00")}:{(myScore % 60).ToString("00")}";

        leaderboardReference = new LeaderboardReference(leaderboardKey);

        LeaderboardCreator.ResetPlayer();
        LoadEntries(leaderboardText); // load leaderboard scores
    }

    public void LoadEntries(TextMeshProUGUI leaderboardText)
    {
        string newLeaderboardText = "";

        leaderboardReference.GetEntries(entries =>
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    string newEntry = $"{entries[i].Rank}. {entries[i].Username} - {(entries[i].Score / 60).ToString("00")}:{(entries[i].Score % 60).ToString("00")} \n";
                    Debug.Log(newEntry);

                    newLeaderboardText += newEntry;

                    //update lowest score
                    if (minScore == 999 || minScore < entries[i].Score)
                        minScore = entries[i].Score;
                }

                Debug.Log(newLeaderboardText);
                leaderboardText.text = newLeaderboardText;

                // try to upload my score
                if (!PlayerPrefs.GetString("name").Equals(""))
                    UploadEntry(PlayerPrefs.GetString("name"), myScore); 
                else
                    UploadEntry("snon200", myScore);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.ToString());
                leaderboardText.text = "Error loading leaderboard";
            }
        });
    }

    public void UploadEntry(string username, int score)
    {
        Debug.Log("username - " + username);
        Debug.Log("upload new score if " + score.ToString() + " < " + minScore.ToString());
        //if new score is in top 5 upload it to database
        if (score < minScore)
        {
            leaderboardReference.UploadNewEntry(username, score, isSuccessful =>
            {
                if (isSuccessful)
                {
                    if (!uploaded)
                    {
                        LoadEntries(leaderboardText);
                        Debug.Log("new score added: " + username + " - " + score.ToString());

                        uploaded = true;
                    }
                }
            });
        }
    }

    public void NextLevel() => SceneManager.LoadScene(nextLevelName); // load next level

    public void RestartLevel() => SingletonClass.instance.RestartLevel(); // restart current level

    public void ExitToMenu() => SingletonClass.instance.ExitToMenu(); // exit to main menu
}
