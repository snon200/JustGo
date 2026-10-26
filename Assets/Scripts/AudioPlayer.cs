using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private float startDelay; // start delay before first clip
    [SerializeField] private float clipDelay; // delay after each clip
    [SerializeField] private GameObject[] toggleOnClipEnd; // objects to toggle on each clip end

    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private float[] clipVolumes;
    private int currentClip = 0;

    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        SingletonClass.instance.SetCursor(false);

        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayNextClip());
    }

    private IEnumerator PlayNextClip()
    {
        if (currentClip == 0) // if first clip
            yield return new WaitForSeconds(startDelay); // wait for start delays
        else
        {
            foreach (GameObject obj in toggleOnClipEnd) // for each objects in array
                obj.SetActive(!obj.activeSelf); // toggle object
        }

        // play audio clip
        audioSource.clip = audioClips[currentClip];
        audioSource.volume = clipVolumes[currentClip];
        audioSource.Play();

        yield return new WaitForSeconds(audioClips[currentClip].length + clipDelay); // wait for clip to end

        if ((currentClip + 1) < audioClips.Length) // not reached last clip
        { 
            currentClip++;
            StartCoroutine(PlayNextClip());
        }
        else // reached last clip
        {
            // play last clip on loop
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
