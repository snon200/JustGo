using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public int time; // current time on the level in seconds

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(CountTimer());
    }

    private IEnumerator CountTimer()
    {
        while (!SingletonClass.instance.gamePaused) // while game is not paused
        {
            yield return new WaitForSeconds(1f); // wait 1 second

            time++; // increase time by 1 second

            timerText.text = $"{(time/60).ToString("00")}:{(time%60).ToString("00")}"; // update timer text
        }
    }
}
