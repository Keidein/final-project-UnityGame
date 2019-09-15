using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {

    private int index
    {
        get {
            if (SceneManager.GetActiveScene().name == "Level3")
            {
                return SceneManager.GetActiveScene().buildIndex - 3;
            } else
            {
                return SceneManager.GetActiveScene().buildIndex + 1;
            }
        }
    }

	public void LoadNextLevel()
	{
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(index);
	}
}
