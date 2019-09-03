using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_bar_controller : MonoBehaviour {

    private Transform bar;
    private float health;
    [SerializeField] private CompletePlayerController player;

    // Use this for initialization
    void Start () {
        bar = transform.Find("bar");
        
    }
	
	// Update is called once per frame
	void Update () {
        health = (float)player.anxiety / 100;
        SetSize(health);
	}

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
