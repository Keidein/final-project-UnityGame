using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelTimer : MonoBehaviour {

	public float levelStartTime = 0f;

	private TextMeshProUGUI textmeshPro;
	//Text theText;

	//private PauseMenu thePauseMenu;

	// Use this for initialization
	void Start () {
		textmeshPro = GetComponent<TMPro.TextMeshProUGUI>();
        
		//thePauseMenu = FindObjectOfType<PauseMenu>();
	}
	
	// Update is called once per frame
	void Update () {
		//if (thePauseMenu.IsPaused)
		//	return;

		levelStartTime -= Time.deltaTime;

		int seconds = Mathf.FloorToInt(levelStartTime % 60);
		int minutes = Mathf.FloorToInt(levelStartTime / 60); 

		//theText.text = "" + Mathf.Round (levelStartTime);

		string timerString = minutes.ToString("00") + ":" + seconds.ToString("00");
		textmeshPro.text = timerString;


	// GAME OVER CONDITION
		if (levelStartTime <= 0)
		{
			FindObjectOfType<GameManager>().EndGame();
		}
	}
}