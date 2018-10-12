﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    public float maxWidth;
    public float leftXPos;

   // public Image healthSprite;
    //public Image maskSprite;

    public float testFrac = 1;

    // So we don't have to keep typing out GetComponent<RectTransform>()
    public RectTransform healthRectTransform;


	// Use this for initialization
	void Start () {

       // rectTransform = gameObject.GetComponent<RectTransform>();

        maxWidth = healthRectTransform.sizeDelta.x;
        leftXPos = healthRectTransform.localPosition.x - maxWidth / 2;



        // healthSprite.GetComponent<RectTransform>()
    }
	
	// Update is called once per frame
	void Update () {

        ResizeBar(testFrac);
   
	}

    public void ResizeBar (float fraction) {
        // fraction should only be between 1 and 0
        Mathf.Clamp(fraction, 0, 1);

        float newWidth = maxWidth * fraction;
        healthRectTransform.sizeDelta = new Vector2 (newWidth, 
                                                     healthRectTransform.sizeDelta.y);

        Vector3 currPos = healthRectTransform.localPosition;
        float newXPos = leftXPos + newWidth / 2;

        healthRectTransform.localPosition = new Vector3(newXPos, currPos.y, currPos.z);




    }
}
