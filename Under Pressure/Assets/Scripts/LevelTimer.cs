using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour {

	public float levelStartTime;

	private Text theText;

	//public GameObject gameOverMenuUI;

	//private PauseMenu thePauseMenu;

	// Use this for initialization
	void Start () {
		//gameOverMenuUI.SetActive(false);
		theText = GetComponent<Text>();

		//thePauseMenu = FindObjectOfType<PauseMenu>();
	}
	
	// Update is called once per frame
	void Update () {
		//if (thePauseMenu.IsPaused)
		//	return;

		levelStartTime -= Time.deltaTime;
		theText.text = "" + Mathf.Round (levelStartTime);

	}
}
