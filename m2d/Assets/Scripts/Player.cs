using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	List<GameObject> tiles = new List<GameObject> ();
	List<GameObject> pool = new List<GameObject> ();

	public string playerName;
	public int playerScore;

	// east, south, west, north
	public string location;

	// hand initialization horizontal coordinate
	public int initHorizontalCoor = -527;

	// hand vertical coordinate
	int Y = -1024;

	public int numAtHand = 0;
	public int numInPool = 0; 

	public int positionX;
	public int positionY;

//	public Text scoreText;

	int width = 84;
	int height = 128;

	int poolInitHorizontalCoor = -250 + 84 / 2;
	int poolInitVerticalCoor = -250 - 128 / 2;


	public GameObject getTile(int i) {
		return tiles [i];
	}

	void addTile (GameObject obj) {
		tiles.Add (obj);
	}
		
	// move the obj to (destX, destY)
	void moveTo(GameObject obj, float xt, float yt) {
		Vector3 v = new Vector3(xt, yt, obj.transform.position.z);
		obj.transform.position = Vector3.MoveTowards (obj.transform.position, v, 10000);
	}
		
	// swap the positions of tiles in display
	void swapDisplay(GameObject obj1, GameObject obj2) {
		float x1 = obj1.transform.position.x;
		float y1 = obj1.transform.position.y;
		float x2 = obj2.transform.position.x;
		float y2 = obj2.transform.position.y;
		moveTo (obj1, x2, y2);
		moveTo (obj2, x1, y1);
	}

	// swap the position of the tiles in the list
	void swap(int i, int j) {
		GameObject temp = tiles[i];
		tiles[i] = tiles[j];
		tiles[j] = temp;
	}
		
	public void discardTileOutFromHand(GameObject tile) {
		pool.Add (tile);
		toPool (tile);
		tiles.RemoveAt(tiles.IndexOf (tile));
		numInPool += 1;
	}

	void toPool(GameObject tile) {
		int h = poolInitHorizontalCoor + (numInPool % 6) * width;
		int v = poolInitVerticalCoor - (numInPool / 6) * height;

		switch (location) {
		case "east":
			moveTo (tile, h, v);
			break;
		case "south":
			moveTo (tile, -v, h);
			break;
		case "west":
			moveTo (tile, -h, -v);
			break;
		case "north":
			moveTo (tile, v, -h);
			break;
		default:
			break;
		}
	}


	// returns true if obj1 <= obj2
	bool compareTiles(GameObject obj1, GameObject obj2) {

		string val1 = obj1.name;
		string val2 = obj2.name;
		List<char> suits = new List<char>(new char[] { 'm', 'p', 's', 'z', 'h' });

		if (suits.IndexOf (val1 [1]) < suits.IndexOf (val2 [1])) {
			return true;
		} else if (suits.IndexOf (val1 [1]) > suits.IndexOf (val2 [1])) {
			return false;
		} else {
			return val1 [0] <= val2 [0];
		}
	}
		
	// Insertion sort. Despite O(n^2), will perform OK when the major part has been sorted. 
	public void sortTiles() {
		int n = tiles.Count;
		Debug.Log (n);
		for (int i = 1; i < n; i++) {
			for (int j = i; j > 0; j--) {
				if (compareTiles (tiles [j], tiles [j - 1])) {
					swapDisplay (tiles [j], tiles [j - 1]);
					swap (j, j - 1);
				} else {
					break;
				}
			}
		}
	}
		
	public void initTileInHand (GameObject tile) {
		int horizontalCoor = initHorizontalCoor + numAtHand * width; 
		int verticalCoor = Y;
		take (tile, horizontalCoor, verticalCoor);
	}

	public void takeTileInHand(GameObject tile) {
		int h = initHorizontalCoor + 13 * width + width / 2;
		int v = Y; 
		take (tile, h, v);

	}
		
	// take one tile to hand
	public void take (GameObject tile, int horizontalCoor, int verticalCoor) {

		Vector3 temp;
		temp.z = -10;
		temp.x = 0;
		temp.y = 0;
		int rotation = 0;

		switch (location) {

		case "east":
			rotation = 0;
			temp.x = horizontalCoor;
			temp.y = verticalCoor;
			break;

		case "south":
			rotation = 90;
			temp.x = -verticalCoor;
			temp.y = horizontalCoor;
			break;

		case "west":
			rotation = 180;
			temp.x = -horizontalCoor;
			temp.y = -verticalCoor;
			break;

		case "north":
			rotation = -90;
			temp.x = verticalCoor;
			temp.y = -horizontalCoor;
			break;

		default:
			break;
		}
			
		GameObject takenTile = Instantiate (tile, temp, Quaternion.Euler (0, 0, rotation));
		takenTile.transform.parent = gameObject.transform;
		addTile (takenTile);
		numAtHand += 1;

	}

	// Use this for initialization
	void Start () {
//		scoreText.text = playerScore.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
