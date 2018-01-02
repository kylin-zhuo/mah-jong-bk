using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	public Mountain mountain;
	public Hand hand;

	// Use this for initialization
	void Start () {
		
		distribute (mountain, hand, 14);
		Debug.Log (hand.tiles.Count);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Distribute n tiles to hand
	void distribute(Mountain mountain, Hand hand, int n) {
		for (int i = 0; i < n; i++) {
			GameObject tile = mountain.take ();
			hand.addTile (tile);
		}
	}
		
}
