 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour {

	public GameObject healthText;
	public int maxHealth;

	int numHealthLeft;


	// Use this for initialization
	void Start () {
		numHealthLeft = maxHealth;
		UpdateHealthText ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void UpdateHealthText() {
		healthText.GetComponent<Text>().text = 
			"HP: " + numHealthLeft + "/" + maxHealth;
	}

	public void SetHealth(int numHealth) {
		numHealthLeft = numHealth;
		UpdateHealthText ();
	}

	public void RestoreToFullHealth() {
		SetHealth (maxHealth);
	}

	public void LoseHealth(int numHealth) {
		int numHealthToLose = Mathf.Min (numHealthLeft, numHealth);

		numHealthLeft -= numHealthToLose;
		UpdateHealthText ();

	}
}
