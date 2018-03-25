using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfig : MonoBehaviour {
	// Player UI Info
	public RectTransform[] playerUIPositions;

	GameObject canvas;

	void Start() {
		canvas = GameObject.FindGameObjectWithTag ("Canvas");
		// CalculatePlayerUIPositions ();

		Debug.Log ("Starting UI position of Player 1 is " + playerUIPositions [0].transform.position);
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
		if (playerNum > 2) {
			Debug.Log ("Error! don't support more than 2 players");
		}
		return playerUIPositions [playerNum - 1].GetComponent<RectTransform>().anchoredPosition;
	}




}
