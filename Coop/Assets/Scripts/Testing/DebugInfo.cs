using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugInfo : MonoBehaviour {

    public Text text;
    public GameObject background;

    string textForThisFrame;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        textForThisFrame = "";

     
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position2d = new Vector2(worldPosition.x, worldPosition.y);
        AddTextThisFrame("mousePosition: " + position2d);

        if (Input.GetKeyDown(KeyCode.L)) {
            text.gameObject.SetActive(!text.IsActive());
            background.SetActive(!background.active);
        }
	}

    private void LateUpdate()
    {
        if (text) {
            text.text = textForThisFrame;
        }
    }

    public void AddTextThisFrame (string s) {
        textForThisFrame += s + "\n";
    }
}
