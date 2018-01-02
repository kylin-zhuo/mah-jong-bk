using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

	// Store the hands 

	public List<GameObject> tiles;
	public List<List<GameObject>> exposures;

	public void addTile (GameObject tile){
		

		Vector3 temp;
		temp.x = -527 + i * 84;
		temp.y = -1024;
		temp.z = -10;

		tiles.Add (Instantiat(tile, temp, ));

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
