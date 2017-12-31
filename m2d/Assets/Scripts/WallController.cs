using UnityEngine;
using System.Collections;
using System;
using Mahjong;
using System.Collections.Generic;



public class WallController : MonoBehaviour {

    private Wall wall;
    [SerializeField]
    private bool showDeadOnly;

    // Use this for initialization
    void Start () {
        wall = new Wall();
    }

    public Tile GetTile()
    {
        return wall.GetTile();
    }

	// Update is called once per frame
	void Update () {
	
	}

}
