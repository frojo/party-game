using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeInfo : MonoBehaviour {

	public GameObject[] spawnPoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 GetPrototypePosition(int playerNum) {
		return spawnPoints [playerNum].transform.position;
	}
}
