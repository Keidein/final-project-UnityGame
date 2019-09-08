using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyBar : MonoBehaviour {

	public Image currentAnxietyBar;
	public Text ratioText;

	private float hitpoint = 150;
	private float maxHitPoint = 150;

	private void Start() 
	{
		UpdateAnxietyBar();
	}

	private void UpdateAnxietyBar()
	{
		float ratio = hitpoint / maxHitPoint;
		currentAnxietyBar.rectTransform.localScale = new Vector3(ratio,1,1);
		ratioText.text = (ratio*100).ToString() + '%';
	}

	private void TakeDamage(float damage)
	{
		
	}

	private void HealDamage(float heal)
	{
		
	}
}
