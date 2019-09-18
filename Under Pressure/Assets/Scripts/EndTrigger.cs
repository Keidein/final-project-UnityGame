using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour {

	public GameManager gameManager;
    public Player p;

	void OnTriggerEnter2D ()
	{
        if (p.locker_done)
        {
            gameManager.CompletedLevel();
        } else 
        if (SceneManager.GetActiveScene().name != "Level3")
        {
            gameManager.CompletedLevel();
        }
	}
}
