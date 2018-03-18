using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OPTI: This could be a big source of opti if you can make the
// input routing stuff happen at compile time as much as possible.
// Maybe you instantiate 8 players at startup, and only show those
// that people want to pl
public class InputMap {

    bool JOYCON_MODE = true;
    bool WINDOWS = false;

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
	public bool GetButton0KeyUp() {
		return Input.GetKeyUp (Button0);
	}
	public bool GetButton0Key() {
		return Input.GetKey (Button0);
	}
	public bool GetButton1KeyDown() {
		return Input.GetKeyDown (Button1);
	}
	public bool GetButton1KeyUp() {
		return Input.GetKeyUp (Button1);
	}
	public bool GetButton1Key() {
		return Input.GetKey (Button1);
	}
	public bool GetButton2KeyDown() {
		return Input.GetKeyDown (Button2);
	}
	public bool GetButton2KeyUp() {
		return Input.GetKeyUp (Button2);
	}
	public bool GetButton2Key() {
		// HACK
		return Input.GetKey (Button2) || Input.GetKey (KeyCode.X);
	}
	public bool GetButton3KeyDown() {
		return Input.GetKeyDown (Button3);
	}
	public bool GetButton3KeyUp() {
		return Input.GetKeyUp (Button3);
	}
	public bool GetButton3Key() {
		// HACK
		return Input.GetKey (Button3) || Input.GetKey(KeyCode.Z);
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
        Debug.Log("Routing Axes for player " + playerNum);
		switch (playerNum) {
		case 1:
            if (JOYCON_MODE) {
                    if (WINDOWS)
                    {
                        leftStickXAxis = "Player1JCSXWin";
                        leftStickYAxis = "Player1JCSYWin";
                    }
                    else
                    {
                        leftStickXAxis = "Player1JCSX";
                        leftStickYAxis = "Player1JCSY";
                    }
            } else {
                leftStickXAxis = "Player1LSX";
                leftStickYAxis = "Player1LSY";
            }
			rightStickXAxis = "Player1RSX";
			rightStickYAxis = "Player1RSY";
			Button0 = KeyCode.Joystick1Button0;
			Button1 = KeyCode.Joystick1Button1;
			Button2 = KeyCode.Joystick1Button2;
			Button3 = KeyCode.Joystick1Button3;
			break;
		case 2:
                Debug.Log("Mapping for player 2!");
            if (JOYCON_MODE)
            {
                    if (WINDOWS)
                    {
                        leftStickXAxis = "Player2JCSXWin";
                        leftStickYAxis = "Player2JCSYWin";
                    }
                    else
                    {
                        leftStickXAxis = "Player2JCSX";
                        leftStickYAxis = "Player2JCSY";
                    }
            }
            else
            {
                leftStickXAxis = "Player2LSX";
                leftStickYAxis = "Player2LSY";
            }
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
	