#define INTERACTIVE_UI_MODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {

    Image portrait;
	PlayerHealthController healthUI;
    UltimateUI ultUI;
	PlayerConfig playerConfig;
	UIConfig uiConfig;


	int abilitiesPlaced = 0;

    // DEV
    public GameObject[] abilityPositions_DEV;
    public GameObject abilitiesParent;


    public void UpdateHealth(int health) {
		healthUI.SetHealth (health);
	}

    public void UpdateUltCharge(float charge)
    {
        ultUI.SetCharge(charge);
    }

    void ApplyPlayerConfig(PlayerConfig player) {
		// Set the position using the player number
		transform.GetComponent<RectTransform>().anchoredPosition = uiConfig.GetPlayerUIPosition (player.number);
		playerConfig = player;

		// Set the color using the player color
	}

	void ApplyCharacterConfig(CharacterConfig character) {
        portrait.sprite = character.portrait;
    }

	void AttachToCanvas() {
		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
		transform.SetParent (canvas.transform);
	}

    public void AttachAbilities(Ability[] abilities)
    {
        foreach (Ability ability in abilities)
        {
            // Sometimes ability is null
            if (!ability || !ability.ui)
            {
                continue;
            }
            AbilityUI ui = ability.ui;

            // Put it in a sane place in Unity hierarchy
            ui.transform.SetParent(abilitiesParent.transform);

            // Place in correct position in UI
            // The only way to tell if this ability has a UI is it if has an icon
            if (ability.icon) {
                ui.GetComponent<RectTransform>().anchoredPosition = abilityPositions_DEV[abilitiesPlaced].GetComponent<RectTransform>().anchoredPosition;
                abilitiesPlaced++;
            }
            // Apply player config
            ui.SetColor(playerConfig.color);
        }
    }


	public void Init(PlayerConfig player, CharacterConfig character, Ability[] abilities, Ultimate ult) {
		gameObject.name = "Player " + player.number+ " UI";
		uiConfig = GameObject.FindGameObjectWithTag ("Canvas").transform.Find ("UIConfig").GetComponent<UIConfig> ();

        healthUI = transform.Find("Health").GetComponent<PlayerHealthController>();
        healthUI.Init(player, character);
        ultUI = transform.Find("UltCharge").GetComponent<UltimateUI>();
		if (ult) {
			ultUI.Init (ult.icon);
		}
        portrait = transform.Find("Portrait").GetComponent<Image>();

        AttachToCanvas ();
		ApplyPlayerConfig (player);
        ApplyCharacterConfig(character);

        AttachAbilities(abilities);


	}

	// Use this for initialization
	void Start () {

		// Set correct position
		// Set correct size
	}
	
	// Update is called once per frame
	void Update () {
#if INTERACTIVE_UI_MODE
		if (playerConfig) {
			transform.localPosition = uiConfig.GetPlayerUIPosition (playerConfig.number);
		}
#endif

    }
}
