using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour {

    // All these should be relative to local position/scale
    public float maxScale;
    public float maxWidth;
    public float leftXPos;

    // TEST
    public float newWidth;
    public float newXPos;
    public Bounds bounds;

    public float testFrac;

    public SpriteRenderer sprite;
    

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();

        maxScale = transform.localScale.x;

        leftXPos = transform.InverseTransformPoint(sprite.bounds.min).x;

        maxWidth = transform.InverseTransformPoint(sprite.bounds.max).x - 
                            transform.InverseTransformPoint(sprite.bounds.min).x;


	}
	
	// Update is called once per frame
	void Update () {
        Resize(testFrac);
		
	}

    // TODO: refactor so that Healthbar.cs and EnemyHealthbar.cs inherit from 
    // eachother in some way
    public void Resize(float fraction) {
        // fraction should be between 0 and 1
        Mathf.Clamp(fraction, 0, 1);

        // newWidth = maxWidth * fraction;
        // sprite.size = new Vector2(newWidth, sprite.size.y);
        float newScale = maxScale * fraction;
        transform.localScale = new Vector3(newScale,
                                           transform.localScale.y,
                                           transform.localScale.z);

        
        // TODO: explain wtf I'm doing
        // Vector3 newWorldMin = sprite.bounds.min;
        newWidth = maxWidth * fraction;
        newXPos = leftXPos + newWidth / 2;
        transform.localPosition = new Vector3(newXPos,
                                              transform.localPosition.y,
                                              transform.localPosition.z);
        



     //   newXPos = leftXPos + newWidth / 2;
        //transform.localPosition = new Vector3(newXPos,
                                              //transform.localPosition.y,
                                              //transform.localPosition.z);

        bounds = sprite.bounds;

      




    }
}
