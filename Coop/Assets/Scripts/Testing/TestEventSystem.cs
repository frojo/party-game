using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is just to test the C# event system
public class TestEventSystem : MonoBehaviour {

    public delegate void TestDelegate();
    public event TestDelegate TestEvent;

    int sharedInt;


    // Use this for initialization
    void Start () {

        for (int i = 0; i < 5000; i++) {
            TestEvent += IncrementSharedInt;
        }

        if (TestEvent != null) {
            TestEvent();
        }
		
	}

    void IncrementSharedInt() {
        sharedInt++;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("SharedInt = " + sharedInt);
		
	}
}
