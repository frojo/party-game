using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour {

	float timeUntilActive = 0f;
	public Text text;

	public void Init(PlayerUIController ui) {
		transform.SetParent (ui.transform);
		text = transform.Find("Text").GetComponent<Text> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CoolDown ();
		text.text = timeUntilActive.ToString ();
	}

	public void SetColor(Color color) {
		text.color = color;
	}

	void StartCoolDown(float duration) {
		timeUntilActive = duration;
	}

	void CoolDown() {
		if (timeUntilActive > 0) {
			timeUntilActive =- Time.deltaTime;
			if (timeUntilActive < 0) {
				timeUntilActive = 0;
			}
		}
	}
}
