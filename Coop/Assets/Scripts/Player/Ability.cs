﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
    public GameObject abilityUIPrefab;

    public float cooldownDuration = 2f;
	protected bool onCooldown = false;

	public AbilityUI ui;

    // Sprite to use for cooldown (if this ability has a UI part)
    public Sprite icon;

    // The Being that this ability belogs to
    public BeingController owner;

	//public abstract void Init ();
	public abstract void Init (BeingController being);

	// TODO: Initialize the UI
	public void Init(BeingController owner, bool hasUI) {
        this.owner = owner;
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
		Transform attackerTransform);
	public virtual void HandleButtonUp (
		Vector2 stickInput,
		Transform attackerTransform) {
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
		onCooldown = false;
	}
}
