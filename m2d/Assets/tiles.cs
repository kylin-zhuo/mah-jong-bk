using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiles : MonoBehaviour {

	public Sprite frontTile;
	public Sprite backTile;

	public string name;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			GetComponent<SpriteRenderer> ().sprite = frontTile;
		}

				
	}
		

}
