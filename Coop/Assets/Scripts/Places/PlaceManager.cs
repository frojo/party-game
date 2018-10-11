using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceManager : MonoBehaviour {
    // Ordered list of Place prefab
    // The order determines what the next place to move to is
    public Place[] places;
    public int currentPlaceNumber = 0;

    public GameObject[] players;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RegisterPlayer(int playerNum, GameObject player) {
        players[playerNum - 1] = player;
    }

    void UnloadPlace(Place place) {
        place.gameObject.SetActive(false);

    }

    void LoadPlace(Place place)
    {
        place.gameObject.SetActive(true);

        // Move players to new place
        for (int i = 0; i < players.Length; i++) {
            if (players[i]) {
                players[i].transform.position = place.playerStartingPositions[i].position;

            }
        }

    }

    // Silently fails if we have no other place to go
    public void GoToNextPlace() {
        // TODO: Do a transition?

        // Don't go to next place
        if (currentPlaceNumber >= places.Length - 1) {
            Debug.LogWarning("Tried to go to next place but there is no next place");
            return;
        }

        UnloadPlace(places[currentPlaceNumber]);
        currentPlaceNumber++;
        LoadPlace(places[currentPlaceNumber]);
    }
}
