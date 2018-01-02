using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	public Mountain mountain;
	public Hand hand;

	public Player[] players;

	// Use this for initialization
	void Start () {
		
//		distribute (mountain, hand, 14);
//		Debug.Log (hand.tiles.Count);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)) {

//			distribute (mountain, players[0], 4);
			initializeHand(mountain);
		}

	}

	// Distribute n tiles to hand
	void distribute(Mountain mountain, Player player, int n) {
		for (int i = 0; i < n; i++) {
			GameObject tile = mountain.take ();
			player.take (tile);
		}
	}

	// Initiate the hand
	void initializeHand(Mountain mountain){
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 4; j++) {
				Player p = players [j];
				distribute (mountain, p, 4);
			}
		}

		for (int i = 0; i < 1; i++) {
			for (int j = 0; j < 4; j++) {
				Player p = players [j];
				distribute (mountain, p, 1);
			}
		}

		distribute (mountain, players [0], 1);
	}
		
}
