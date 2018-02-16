using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfig : MonoBehaviour {

	static int MAX_PLAYERS = 4;
	static float CANVAS_X_MARGIN_FRACTION = .1f;
	static float CANVAS_Y_MARGIN_FRACTION = .1f;
	float canvasXMarginLength;
	float canvasYMarginLength;
	float intervalLength;

	// Player UI Info
	public Vector3[] playerUIPositions;

	GameObject canvas;

	void Start() {
		canvas = GameObject.FindGameObjectWithTag ("Canvas");
		// CalculatePlayerUIPositions ();
	}

	// void CalculatePlayerUIPositions() {
	// 	float canvasHeight = canvas.GetComponent<RectTransform> ().sizeDelta.y;
	// 	canvasYMarginLength = canvasHeight * CANVAS_Y_MARGIN_FRACTION;
	// 	Debug.Log ("Canvas Y Margin = " + canvasYMarginLength);

	// 	float canvasWidth = canvas.GetComponent<RectTransform> ().sizeDelta.x;
	// 	canvasXMarginLength = canvasWidth * CANVAS_X_MARGIN_FRACTION;
	// 	Debug.Log ("Canvas X Margin = " + canvasXMarginLength);
	// 	intervalLength = (canvasWidth - 2 * canvasXMarginLength) / MAX_PLAYERS;
	// 	Debug.Log ("Interval Length = " + intervalLength);

	// 	for (int i = 0; i < MAX_PLAYERS; i++) {
	// 		playerUIPositions [i] = new Vector3 (
	// 			canvasXMarginLength + i * intervalLength, 
	// 			canvasYMarginLength, 0);
	// 	}
	// }

	public Vector3 GetPlayerUIPosition(int playerNum) {
		return playerUIPositions [playerNum - 1];
	}




}
