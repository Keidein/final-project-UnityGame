using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {

	public float levelStartTime;

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
		theText.text = "" + Mathf.Round (levelStartTime);
	}
}
