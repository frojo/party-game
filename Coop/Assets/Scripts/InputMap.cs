﻿#define JOYCON_MODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OPTI: This could be a big source of opti if you can make the
// input routing stuff happen at compile time as much as possible.
// Maybe you instantiate 8 players at startup, and only show those
// that people want to play
public class InputMap {

	// E.g. 1 for player 1, 2 for player 2 etc.
	public int playerNum;

	// Left stick axes
	string leftStickXAxis;
	string leftStickYAxis;

	// Right stick axes
	public string rightStickXAxis;
	public string rightStickYAxis;

	// Face buttons
	// X, Square etc.
	KeyCode Button0;
	// Confirm button (A, X etc.)
	KeyCode Button1;
	// Cancel button (B, Circle etc.)
	KeyCode Button2;
	// Y, Triangle etc.
	KeyCode Button3;

	// Triggers


	public bool GetButton0KeyDown() {
		return Input.GetKeyDown (Button0);
	}
	public bool GetButton0Key() {
		return Input.GetKey (Button0);
	}
	public bool GetButton1KeyDown() {
		return Input.GetKeyDown (Button1);
	}
	public bool GetButton1Key() {
		return Input.GetKey (Button1);
	}
	public bool GetButton2KeyDown() {
		return Input.GetKeyDown (Button2);
	}
	public bool GetButton2Key() {
		return Input.GetKey (Button2);
	}
	public bool GetButton3KeyDown() {
		return Input.GetKeyDown (Button3);
	}
	public bool GetButton3Key() {
		return Input.GetKey (Button3);
	}
	public float GetStickXAxis() {
		return Input.GetAxis (leftStickXAxis);
	}

	public float GetStickYAxis() {
		return Input.GetAxis (leftStickYAxis);
	}
	public Vector2 Get2DStickInput() {
		return new Vector2 (
			Input.GetAxis (leftStickXAxis),
			Input.GetAxis (leftStickYAxis));
	}

	public InputMap(int playerNum) {
		RouteAxes (playerNum);
	}
		

	void RouteAxes(int playerNum) {
		switch (playerNum) {
		case 1:
			#if JOYCON_MODE
			leftStickXAxis = "Player1JCSX";
			leftStickYAxis = "Player1JCSY";
			#else
			leftStickXAxis = "Player1LSX";
			leftStickYAxis = "Player1LSY";
			#endif
			rightStickXAxis = "Player1RSX";
			rightStickYAxis = "Player1RSY";
			Button0 = KeyCode.Joystick1Button0;
			Button1 = KeyCode.Joystick1Button1;
			Button2 = KeyCode.Joystick1Button2;
			Button3 = KeyCode.Joystick1Button3;
			break;
		case 2:
			#if JOYCON_MODE
			leftStickXAxis = "Player2JCSX";
			leftStickYAxis = "Player2JCSY";
			#else
			leftStickXAxis = "Player2LSX";
			leftStickYAxis = "Player2LSY";
			#endif
			rightStickXAxis = "Player2RSX";
			rightStickYAxis = "Player2RSY";
			Button0 = KeyCode.Joystick2Button0;
			Button1 = KeyCode.Joystick2Button1;
			Button2 = KeyCode.Joystick2Button2;
			Button3 = KeyCode.Joystick2Button3;
			break;
		default:
			Debug.Log ("Not a supported player number!");
			break;
		}

	
	}
}
	