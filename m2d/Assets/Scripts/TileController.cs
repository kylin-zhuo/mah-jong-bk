using UnityEngine;
using System.Collections;
using Mahjong;
using UnityEngine.UI;

public class TileController : MonoBehaviour {

 //   private bool isVisible=false;

    private Tile value;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetTile(Tile value)
    {
        this.value = value;

        GetComponent<Image>().sprite = this.value.face;
    }
}
