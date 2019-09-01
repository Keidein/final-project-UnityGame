using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour {

	public float levelStartTime = 0f;

	private Text theText;

	//private PauseMenu thePauseMenu;

	// Use this for initialization
	void Start () {
		theText = GetComponent<Text>();

		//thePauseMenu = FindObjectOfType<PauseMenu>();
	}
	
	// Update is called once per frame
	void Update () {
		//if (thePauseMenu.IsPaused)
		//	return;

		levelStartTime -= Time.deltaTime;

		int seconds = (int)(levelStartTime % 60);
		int minutes = (int)(levelStartTime / 60) % 60; 

		//theText.text = "" + Mathf.Round (levelStartTime);

		string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
		theText.text = timerString;

		if (levelStartTime <= 0)
		{
			FindObjectOfType<GameManager>().EndGame();
		}
	}
}