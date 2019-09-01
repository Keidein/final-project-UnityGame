using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject gameOverMenuUI;
	bool gameHasEnded = false;
	public float restartDelay = 5f;

	public void EndGame ()
	{
		if (gameHasEnded == false)
		{
			gameHasEnded = true;
			Debug.Log("GAME OVER");
			GameOver();
			//Invoke("Restart", restartDelay);
		}	


	}


	public void GameOver ()
	{
		gameOverMenuUI.SetActive(true);
		Time.timeScale = 0f;
	}

	public void Restart ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1f;
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("MainMenu");
		Time.timeScale = 1f;
	}
}
