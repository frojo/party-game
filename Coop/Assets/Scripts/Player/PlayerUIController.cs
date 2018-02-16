#define INTERACTIVE_UI_MODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour {

	PlayerHealthController healthUI;
	PlayerConfig playerConfig;
	UIConfig uiConfig;

	int abilitiesAttached = 0;
	static int MAX_ABILITIES = 4;
	static float ABILITY_MARGIN_FRACTION = .1f;
	float abilityIntervalLength;


	public void UpdateHealth(int health) {
		Debug.Log ("Updating health for " + name);
		healthUI.SetHealth (health);
	}
		
	void ApplyPlayerConfig(PlayerConfig player) {
		// Set the position using the player number
		transform.localPosition = uiConfig.GetPlayerUIPosition (player.number);
//		transform.position = 
		playerConfig = player;

		// Set the color using the player color
	}

	void ApplyCharacterConfig(CharacterConfig character) {
		
	}

	void AttachToCanvas() {
		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
		transform.SetParent (canvas.transform);
	}

	public void AttachAbilityUI(GameObject abilityUIObj) {
		AbilityUI abilityUI = abilityUIObj.GetComponent<AbilityUI> ();

		// Position it correctly
		float width = gameObject.GetComponent<RectTransform>().sizeDelta.x;
		abilityIntervalLength = width * (1 - 2 * ABILITY_MARGIN_FRACTION) / MAX_ABILITIES;
		abilityUIObj.transform.position = transform.position + Vector3.right * abilityIntervalLength;

		// Apply player config
		abilityUI.SetColor(playerConfig.color);

		abilitiesAttached++;
	}

	public void Init(PlayerConfig player, CharacterConfig character) {
		gameObject.name = "Player " + player.number+ " UI";
		uiConfig = GameObject.FindGameObjectWithTag ("Canvas").transform.Find ("UIConfig").GetComponent<UIConfig> ();

		AttachToCanvas ();
		ApplyPlayerConfig (player);

		healthUI = transform.Find ("Health")
			.GetComponent<PlayerHealthController> ();
		healthUI.Init (player, character);
	}

	// Use this for initialization
	void Start () {

		// Set correct position
		// Set correct size
	}
	
	// Update is called once per frame
	void Update () {
#if INTERACTIVE_UI_MODE
		transform.localPosition = uiConfig.GetPlayerUIPosition (playerConfig.number);
#endif

    }
}
