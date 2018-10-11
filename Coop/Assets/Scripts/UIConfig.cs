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

	public Vector3 GetPlayerUIPosition(int playerNum) {
		if (playerNum > 2) {
			Debug.Log ("Error! don't support more than 2 players");
		}
		return playerUIPositions [playerNum - 1].GetComponent<RectTransform>().anchoredPosition;
	}




}
