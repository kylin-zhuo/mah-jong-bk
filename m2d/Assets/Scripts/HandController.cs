using UnityEngine;
using System.Collections;
using Mahjong;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class HandController : MonoBehaviour {

    [SerializeField]
    GameObject prefab;

    [SerializeField]
    WallController wall;

    Hand hand;

    [SerializeField]
    Text wait;

	// Use this for initialization
	void Start () {
        GameObject tile;
            hand = new Hand();
            //yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 13; i++)
            {
                hand.GetFromWall();
            }

            Tile[] temp = hand.isTempai();
            if (temp.GetLength(0) > 0)
                wait.text = temp[0].ToString();
            else
                wait.text = "No wait((((((((";
        hand.Sort();

        for (int i = 0; i < 13; i++)
        {
            tile = Instantiate(prefab);
            tile.transform.SetParent(transform, false);
            tile.GetComponent<TileController>().SetTile(hand.GetTile(i));
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
