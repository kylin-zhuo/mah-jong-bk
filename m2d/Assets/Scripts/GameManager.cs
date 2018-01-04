using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	public Mountain mountain;
	public Player[] players;

	// Use this for initialization
	void Start () {
		
//		distribute (mountain, hand, 14);
//		Debug.Log (hand.tiles.Count);
		
	}

	int np = 0;

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)) {
			initializeHand(mountain);
//			Debug.Log (players [0].getTile (6).transform.position.x);
//			Debug.Log (players [0].getTile (6).transform.position.y);
			players [0].sortTiles ();
			players [1].sortTiles ();
			players [2].sortTiles ();
			players [3].sortTiles ();

			Debug.Log (players [0].getTile (6).GetComponentInParent<Player>().name);
		}

		if (Input.GetKeyDown (KeyCode.A)) {
//			GameObject t = players [1].getTile (5);
//			Debug.Log (t.transform.position);

			GameObject t = mountain.take();
			players [np].takeTileInHand (t);
//			t.transform.Translate(0, 400, 0);
//			Debug.Log (t.GetComponentInParent<Player> ().transform.position);

		}

		if (Input.GetKeyDown (KeyCode.B)) {

			GameObject t = players [np].getTile (13);
			Debug.Log (t.transform.position);
			players [np].discardTileOutFromHand (t);
			Debug.Log (t.transform.position);
			np = (np + 1) % 4;

		}
	}


	// Distribute n tiles to hand
	void distribute(Mountain mountain, Player player, int n) {
		for (int i = 0; i < n; i++) {
			GameObject tile = mountain.take ();
			player.initTileInHand (tile);
		}
	}

	// Initiate the hand
	void initializeHand(Mountain mountain){
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 4; j++) {
				distribute (mountain, players [j], 4);
			}
		}
		for (int j = 0; j < 4; j++) {
			distribute (mountain, players [j], 1);
		}
//		distribute (mountain, players[0], 1);
	}
		

}
