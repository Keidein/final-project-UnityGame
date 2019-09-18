using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject gameOverMenuUI;
	public GameObject completeLevelUI;

	bool gameHasEnded = false;
	public float restartDelay = 5f;

    private int index {
        get {
            if (SceneManager.GetActiveScene().name == "Level3")
            {
                return SceneManager.GetActiveScene().buildIndex - 4;
            }
            else
            {
                return SceneManager.GetActiveScene().buildIndex + 1;
            }
        }
    }

    // If level is completed
    public void CompletedLevel ()
	{
		completeLevelUI.SetActive(true);
		Debug.Log("LEVEL WON!");	
	}

	// If game is lost
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

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(index);
    }
}
