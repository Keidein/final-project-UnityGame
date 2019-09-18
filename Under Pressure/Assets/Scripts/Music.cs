using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public AudioSource music;
    public AudioSource anxiousMusic;

    public Player player;

    /*void Awake()
    {
        anxiousMusic.Stop();
    }*/

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
        music.Play();
        music.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    void Update()
    {
        if (player.anxiety >= 75 && music.isPlaying && !anxiousMusic.isPlaying)
        {
            music.Stop();
            anxiousMusic.Play();
        }

        if (player.anxiety < 75 && !music.isPlaying && anxiousMusic.isPlaying)
        {
            anxiousMusic.Stop();
            music.Play();
        }
    }
}