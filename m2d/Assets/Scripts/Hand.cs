using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

	// Store the hands 

	List<GameObject> tiles = new List<GameObject> ();
//	List<List<GameObject>> exposures = new List<List<GameObject>> ();

	public int leftCoordinate = -527;
	public int num;
	string location;

	public GameObject getTile(int i) {
		return tiles [i];
	}

	public void addTile (GameObject tile) {
		tiles.Add (tile);
		num += 1;
	}

	public void addTiles(List<GameObject> tiles) {
		foreach (GameObject tile in tiles) {
			addTile (tile);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
