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

    // This lets other classes subscribe to the death of this being
    public delegate void DeathAction();
    public event DeathAction OnDied;

    // Used to figure out which objects to collide with
    LayerMask onlyBounds;
    // Used to avoid bug of Being not being able to slide a long a Bound
    public float cornerEpsilon = 0.1f;

    // Use this for initialization
    void Start () {

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

            // This is the change in position before accounting for collisions
            Vector2 rawPositionDelta = leftStickInput * speed;
      
            UpdatePosition(rawPositionDelta);
        }
	}

    public void UpdatePosition(Vector2 rawPositionDelta) {
        Bounds bounds = gameObject.GetComponent<BoxCollider2D>().bounds;

        Vector3 positionDelta = new Vector3(
                          GetPositionDeltaOneDirection(rawPositionDelta.x, bounds, true),
                          GetPositionDeltaOneDirection(rawPositionDelta.y, bounds, false), 0);

        transform.position += positionDelta;

    }

    // rawPositionDelta: Change in position in one direction before account for collisions
    // bounds: Bounds of the Being's bounding box
    // horizontal: Whether this is horizontal or vertical movment
    float GetPositionDeltaOneDirection(float rawPositionDelta, Bounds bounds, bool horizontal) {
        // This garbage is what I get for trying to implement collisions myself.

        // Corners of our bounding box. Will be used to find the end points of 
        // the forward facing edge
        Vector2 topRight = bounds.max;
        Vector2 bottomLeft = bounds.min;
        Vector2 topLeft = new Vector2(bounds.min.x, bounds.max.y);
        Vector2 bottomRight = new Vector2(bounds.max.x, bounds.min.y);

        // Figure out the "left" and "right" points of the forward-facing edge 
        // of our moving Being (in direction of movement) and isolate the 
        // moveDirection to one axis.
        // "Left" and "right" are from the perspective of somebody moving in
        // the same direction that Being is moving.
        Vector2 leftCorner;
        Vector2 rightCorner;
        Vector2 moveDirection;
        if (horizontal) {
            if (rawPositionDelta > 0) {
                // Moving right
                leftCorner = topRight + Vector2.down * cornerEpsilon;
                rightCorner = bottomRight + Vector2.up * cornerEpsilon;
            } else {
                // Moving left
                leftCorner = bottomLeft + Vector2.up * cornerEpsilon;
                rightCorner = topLeft + Vector2.down * cornerEpsilon;
            }
            moveDirection.x = rawPositionDelta;
            moveDirection.y = 0;

        } else {
            if (rawPositionDelta > 0)
            {
                // Moving up
                leftCorner = topLeft + Vector2.right * cornerEpsilon;
                rightCorner = topRight + Vector2.left * cornerEpsilon;
            }
            else
            {
                // Moving down
                leftCorner = bottomRight + Vector2.left * cornerEpsilon;
                rightCorner = bottomLeft + Vector2.right * cornerEpsilon;
            }
            moveDirection.x = 0;
            moveDirection.y = rawPositionDelta;
        }

        // Raycast from both front-facing corners in the direction that we're 
        // moving
        RaycastHit2D leftHit = Physics2D.Raycast(leftCorner,
                                             moveDirection,
                                             10f, onlyBounds);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCorner,
                                           moveDirection,
                                           10f, onlyBounds);


        // Find the closest collision distance 
        float leftCollisionDistance = leftHit.collider == null ? Mathf.Infinity : leftHit.distance;
        float rightCollisionDistance = rightHit.collider == null ? Mathf.Infinity : rightHit.distance;


        //// DEBUG
        //if (name == "Player 1 (Tank)" && !horizontal)
        //{
        //    gameController.debugInfo.AddTextThisFrame(
        //        "rawPositionDelta: " + rawPositionDelta);
        //    gameController.debugInfo.AddTextThisFrame(
        //        "direction: " + direction);
        //    gameController.debugInfo.AddTextThisFrame(
        //        "leftCorner: " + leftCorner);
        //    gameController.debugInfo.AddTextThisFrame(
        //        "rightCorner: " + rightCorner);
        //    gameController.debugInfo.AddTextThisFrame(
        //        "leftCollisionDistance: " + leftCollisionDistance);
        //    if (leftHit.collider != null) {
        //        gameController.debugInfo.AddTextThisFrame(
        //            "leftHit.collider: " + leftHit.collider.name);

        //    }
        //    gameController.debugInfo.AddTextThisFrame(
        //        "rightCollisionDistance: " + rightCollisionDistance);
        //    if (rightHit.collider != null)
        //    {
        //        gameController.debugInfo.AddTextThisFrame(
        //            "rightHit.collider: " + rightHit.collider.name);

        //    }

        //}

        // Move player the min(distanceToCollider, rawMoveDelta)
        float absPositionDelta = Mathf.Min(leftCollisionDistance, 
                                           rightCollisionDistance, 
                                           Mathf.Abs(rawPositionDelta));

        return absPositionDelta * (rawPositionDelta < 0 ? -1 : 1);
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
        // Let any subscribers know that this died
        if (OnDied != null)
        {
            OnDied();
        }
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

	public Hitbox GetHitbox() {
		return transform.Find ("Hitbox").GetComponent<Hitbox> ();
	}

	public void Init(CharacterConfig character) {
		//Debug.Log ("Initing player " + playerNum + " as a " + character.name);
		//gameObject.name = "Player " + playerNum + " (" + character.name + ")";
		ApplyCharacterConfig (character);
        gameController = GameObject.FindObjectOfType<GameController>();
        //inputMap = new InputMap(0);
        onlyBounds = LayerMask.GetMask("Bounds");
        Debug.Log("onlyBounds: " + onlyBounds.value);

    }

}
