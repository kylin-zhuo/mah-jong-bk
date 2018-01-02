using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public string tileName;

	public Sprite backTile;
	public Sprite frontTile;

//	public Sprite face { get; private set; }
//
//	public Tile(string name) {
//		tileName = name;
//		face = LoadImage ();
//	}
//
//	//should be in Unity part
//	public Sprite LoadImage()
//	{
//		string path = "mahjong-icons/" + tileName;
//		Sprite pic = Resources.Load<Sprite>(path);
//		return pic;
//	}

	// Use this for initialization
	void Start () {
//		GetComponent<SpriteRenderer> ().sprite = backTile;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
