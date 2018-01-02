using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Hand hand = new Hand();

	// east, south, west, north
	public string location;

	public int leftCoordinate = -527;
	public int num = 0;


	public void take (GameObject tile){

		int Y = 1024;
		int width = 84;

		Vector3 temp;
		temp.z = -10;
		temp.x = 0;
		temp.y = 0;

		int rotation = 0;

		switch (location) {

		case "east":
			rotation = 0;
			temp.x = leftCoordinate + num * width;
			temp.y = -Y;
			break;

		case "south":
			rotation = -90;
			temp.x = -Y;
			temp.y = leftCoordinate + num * width;
			break;

		case "west":
			rotation = 180;
			temp.x = -leftCoordinate - num * width;
			temp.y = Y;
			break;

		case "north":
			rotation = 90;
			temp.x = Y;
			temp.y = -leftCoordinate - num * width;
			break;

		default:
			break;
		}
			
		hand.addTile (Instantiate(tile, temp, Quaternion.Euler(0, 0, rotation)));
		num += 1;

	}

	void UpdateView() {
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
