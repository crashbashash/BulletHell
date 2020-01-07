using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public List<AudioClip> bgmClips;
    private AudioSource audioSource;
    private int curSong = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmClips[curSong];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (curSong >= bgmClips.Count - 1)
                curSong = 0;
            else
                curSong++;
            audioSource.clip = bgmClips[curSong];
            audioSource.Play();
        }
    }
}
