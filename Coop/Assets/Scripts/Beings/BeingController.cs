using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingController : MonoBehaviour {

	public static int NUM_ABILITIES = 4;

    public float normalSpeed = 1f;
	public float speed = 1f;

    public int maxHealth;
	public int healthPoints;
    public float ultCharge;
    public bool ultChargeFrozen = false;

	public Vector2 currentDirection;
	public bool facingRight = true;

	public int stunCounter = 0;

	protected GameController gameController;
	private InputMap inputMap;

	public Ability[] abilities;
    public Ultimate ult;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindObjectOfType<GameController> ();
		inputMap = new InputMap (0);
	}

	// Update is called once per frame
	void Update () {
		if (!IsStunned()) {
			Vector2 leftStickInput = inputMap.Get2DStickInput ();

			UpdateCurrentDirection (leftStickInput);
			UpdateAbilities (leftStickInput);

			// Do movement
			Vector3 leftStickInput3d = 
				new Vector3 (leftStickInput.x, leftStickInput.y, 0);
			transform.position += leftStickInput3d * speed;
		}
	}

	void UpdateCurrentDirection(Vector2 stickInput) {
		if (stickInput != Vector2.zero) {
			currentDirection = stickInput.normalized;
		}
		facingRight = (currentDirection.x >= 0);
	}

	//void DoAbility(A
	void UpdateAbilities(Vector2 leftStickInput) {

		if (inputMap.GetButton0KeyDown()) {
			Debug.Log("Bottom!");
			if (abilities[0]) {
				abilities[0].HandleButtonDown (leftStickInput, transform);
			}
		}
		if (inputMap.GetButton1KeyDown()) {
			Debug.Log("Right!");

		}
		if (inputMap.GetButton2KeyDown()) {
			// This is the attack button
			if (abilities[2]) {
				abilities[2].HandleButtonDown (leftStickInput, transform);
			}

			Debug.Log("Left!");
		}
		if (inputMap.GetButton3KeyDown()) {
			Debug.Log("Top!");
		}

	}

	public bool IsDead() {
		return (healthPoints <= 0);
	}

	void Die() {
		Destroy (gameObject);
	}

	public void KnockBack(Vector2 direction, float magnitude) {
		transform.position += new Vector3(direction.x, direction.y, 0) * magnitude;
	}

	public void Stun(float duration) {
		stunCounter++;
		StartCoroutine (_RecoverFromStun (duration));
	}

	public bool IsStunned() {
		return stunCounter > 0;
	}

	IEnumerator _RecoverFromStun(float duration) {
		yield return new WaitForSeconds (duration);
		stunCounter--;
	}

    public void Slow(float duration, float slowness)
    {
        speed = speed * slowness;
        StartCoroutine(_RecoverFromSlow(duration));
    }

    IEnumerator _RecoverFromSlow(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed = normalSpeed;
    }

    public virtual int TakeDamage(int damage) {
        int damageTaken = Mathf.Min(damage, healthPoints);

        healthPoints = healthPoints - damage;

		if (healthPoints < 0) {
			healthPoints = 0;
		}
		if (IsDead()) {
			Die ();	
		}

        return damageTaken;
	}

    public virtual int Heal(int amount)
    {
        int amountHealed = Mathf.Min(maxHealth - healthPoints, amount);
        healthPoints += amount;

        if (healthPoints > maxHealth)
        {
            healthPoints = maxHealth;
        }

        return amountHealed;
    }

    public virtual void AddUltCharge(float ammount)
    {
        if (ultChargeFrozen)
        {
            return;
        }
        ultCharge += ammount;
    }

    public virtual void ResetUltCharge()
    {
        ultCharge = 0;
    }
    
    public virtual void FreezeUltCharge()
    {
        ultChargeFrozen = true;
    }

    public virtual void UnfreezeUltCharge()
    {
        ultChargeFrozen = false;
    }

    // TODO: Freeze/Unfreeze ult charge functions

	void AttachAbility(int ability_num, CharacterConfig character) {
		if (!character.abilities[ability_num]) {
			return;
		}
		GameObject abilityObj = Instantiate (character.abilities[ability_num]);
		abilityObj.transform.parent = transform;
		Ability ability = abilityObj.GetComponent<Ability> ();
		ability.Init (this);
		abilities [ability_num] = ability;
	}

    void AttachUlt(CharacterConfig character) {
		if (!character.ultimate) {
			return;
		}
        GameObject ultObj = Instantiate(character.ultimate);
        ultObj.transform.parent = transform;
        Ultimate ult = ultObj.GetComponent<Ultimate>();
        ult.Init(this);
        this.ult = ult;
    }

	public virtual void ApplyCharacterConfig(CharacterConfig character) {
		maxHealth = healthPoints = character.healthPoints;
		transform.localScale *= character.size;
		normalSpeed = speed = character.speed;

		// Attach abilities
		for (int i = 0; i < NUM_ABILITIES; i++) {
			AttachAbility (i, character);
		}
        AttachUlt(character);
    }

	public void Init(CharacterConfig character) {
		//Debug.Log ("Initing player " + playerNum + " as a " + character.name);
		//gameObject.name = "Player " + playerNum + " (" + character.name + ")";
		ApplyCharacterConfig (character);

	}

}
