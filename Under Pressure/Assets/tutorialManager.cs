using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialManager : MonoBehaviour {

    public Button tutorialButton;
    public Button mainMenu;
    public GameObject controls_text;
    public GameObject gameplay_text;
    public Text button_text;

    private bool controls;


    // Use this for initialization
    void Start () {
        controls = false;

        tutorialButton.onClick.AddListener(tutorialTextChange);
    }
	
	// Update is called once per frame
	void Update () {
		if (controls == false)
        {
            controls_text.SetActive(false);
            gameplay_text.SetActive(true);
            button_text.text = "Controls";

        } else if (controls == true)
        {
            controls_text.SetActive(true);
            gameplay_text.SetActive(false);
            button_text.text = "Gameplay";
        }
	}

    void tutorialTextChange()
    {
        if (controls == false)
        {
            controls = true;
        } else if (controls == true)
        {
            controls = false;
        }
    }
}
