 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour {

	public GameObject healthText;
    public Healthbar bar;
	public int maxHealth;

	int numHealthLeft;

	public void Init(PlayerConfig player, CharacterConfig character) {
		// healthText.GetComponent<Text> ().color = player.color;
        bar.Init(player.color);

		maxHealth = character.healthPoints;
		numHealthLeft = maxHealth;
		UpdateHealthText ();
	}

	// Use this for initialization
	void Start () {
//		numHealthLeft = maxHealth;
//		UpdateHealthText ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void UpdateHealthText() {
		healthText.GetComponent<Text>().text = 
			"HP: " + numHealthLeft + "/" + maxHealth;
	}

	public void SetHealth(int numHealth) {
        Debug.Log("Setting health to " + numHealth);
		numHealthLeft = numHealth;
		UpdateHealthText ();
        bar.ResizeBar((float)numHealthLeft / maxHealth);
	}

	public void RestoreToFullHealth() {
		SetHealth (maxHealth);
	}
}
