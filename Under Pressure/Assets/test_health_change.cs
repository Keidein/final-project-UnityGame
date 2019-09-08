using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_health_change : MonoBehaviour {

    [SerializeField] private health_bar_controller healthBar;

	// Use this for initialization
	void Start () {
        healthBar.SetSize(100f);
	}

}
