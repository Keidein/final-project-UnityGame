using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	public AudioSource music;
	public Slider volume;


	// Use this for initialization
	void Start () {
		volume.value = PlayerPrefs.GetFloat("MusicVolume");
	}
	
	// Update is called once per frame
	void Update () {
		music.volume = volume.value;
	}

	public void VolumePrefs()
	{
		PlayerPrefs.SetFloat("MusicVolume", music.volume);
		PlayerPrefs.Save();
	}
}
