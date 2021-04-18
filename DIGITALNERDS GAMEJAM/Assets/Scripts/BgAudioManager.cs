using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgAudioManager : MonoBehaviour
{
    public static bool playBgMusic = false;
    public AudioSource audioSource;
    public AudioClip bgMusic;
    // Start is called before the first frame update
    void Start()
    {
        playBgMusic = true;
        audioSource.clip = bgMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(playBgMusic == false){
            audioSource.Pause();
        }
    }
}
