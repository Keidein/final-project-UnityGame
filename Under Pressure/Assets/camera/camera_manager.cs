using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_manager : MonoBehaviour {

    public Camera normal;
    public Camera greyscale;
    public Player player; 

    // Use this for initialization
    void Start () {
        normal.enabled = true;
        greyscale.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (player.anxiety < 75)
        {
            
            normal.enabled = true;
            greyscale.enabled = false;
        } else
        {
            normal.enabled = false;
            greyscale.enabled = true;
        }
    }
}
