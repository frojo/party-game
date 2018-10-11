using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour {

	public float timeUntilActive = 0f;
	public Text text;
    public Image image;

	public void Init(Sprite icon) {

        // TODO: set image's icon to be "icon"
        image.sprite = icon;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCoolDown ();

        UpdateView();
	}

    void UpdateView()
    {
        if (timeUntilActive > 0)
        {
            // Make sure that it's transparent/masked

            // Make sure text is enabled and update text
            text.enabled = true;
            text.text = timeUntilActive.ToString("F0");

        } else
        {
            // Make sure that it's not transparent
            // Make sure text is disabled

            text.enabled = false;
        }
    }

	public void SetColor(Color color) {
		text.color = color;
	}

	public void StartCoolDown(float duration) {
        timeUntilActive = duration;

        Debug.Log("Starting UI cooldown on " + gameObject.name + "with duration = " + duration);

        // Show text
	}

	void UpdateCoolDown() {
		if (timeUntilActive > 0) {
			timeUntilActive -= Time.deltaTime;
			if (timeUntilActive < 0) {
				timeUntilActive = 0;
			}
		}
        
		text.text = timeUntilActive.ToString ();
	}
}
