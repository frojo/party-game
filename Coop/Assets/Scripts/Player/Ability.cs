using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
    public GameObject abilityUIPrefab;

    public float cooldownDuration = 2f;
	protected bool onCooldown = false;

	public AbilityUI ui;

    // Sprite to use for cooldown (if this ability has a UI part)
    public Sprite icon;

	//public abstract void Init ();
	public abstract void Init ();

	// TODO: Initialize the UI
	public void Init(bool hasUI) {
        // If abilityUI is not null, then this ability has a UI associated with it
        if (hasUI) {
            // Instantiate AbilityUI Obj
            abilityUIPrefab = FindObjectOfType<GameController>().abilityUIPrefab;
            ui = Instantiate(abilityUIPrefab).GetComponent<AbilityUI>();
            ui.Init (icon);
            // ui = newAbilityUIObj
        }
    }

	public abstract void HandleButtonDown (
		Vector2 stickInput,
		Transform characterTransform);
	public virtual void HandleButtonUp (
		Vector2 stickInput,
		Transform characterTransform) {
	}

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	// Returns false if the ability is currently on cooldown
	public bool StartCooldown() {
		// Do nothing if ability is currenly on cooldown
		if (onCooldown) {
			return false;
		}
		onCooldown = true;

		if (ui) {
			ui.StartCoolDown (cooldownDuration);
		}
		StartCoroutine (CooldownTimer (cooldownDuration));
		return true;
	}
		
	IEnumerator CooldownTimer(float duration) {
		yield return new WaitForSeconds (duration);
		Debug.Log ("Cooldown timer ending for " + name);
		onCooldown = false;
	}
}
