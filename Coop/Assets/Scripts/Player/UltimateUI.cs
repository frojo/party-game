using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateUI : MonoBehaviour {

    public Text text;
    public Image image;

    public void Init(Sprite icon)
    {
        image.sprite = icon;
        SetCharge(0);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCharge(float charge)
    {
        if (charge > 100)
        {
            charge = 100;
        }
        text.text = string.Concat(charge.ToString("F0"), "%");
    }
}
