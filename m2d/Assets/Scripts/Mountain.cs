using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : MonoBehaviour {

	// initialized in UI
	public GameObject[] mpsz; 
	public GameObject[] h; 
	public GameObject back;

	private List<GameObject> allBacks = new List<GameObject> ();
	private List<GameObject> allFronts = new List<GameObject> ();

	private int start;
	private int position = 0;
//	private int last = 143;


	public GameObject take() {

		GameObject ret = allFronts [position];
		GameObject.Destroy (allBacks [position]);
		position += 1;
		return ret;
	}

	private void Shuffle(List<GameObject> ts) {
		var count = ts.Count;
		var last = count - 1;
		for (var i = 0; i < last; ++i) {
			var r = UnityEngine.Random.Range(i, count);
			var tmp = ts[i];
			ts[i] = ts[r];
			ts[r] = tmp;
		}
	}

	// generate all set of tiles
	void InitializeFront() {

		foreach (GameObject obj in mpsz) {

			for (int i = 0; i < 4; i++) {
				allFronts.Add (obj);
			}
		}

		foreach (GameObject obj in h) {
			allFronts.Add (obj);
		}

		Shuffle(allFronts);

	}
		

	void Initialize() {

		// generate the wall in the top
		for (int i = 0; i < 36; i++) {

			Vector3 tilePosition;
			tilePosition.x = -700 + (i/2) * 82;

			if (i % 2 == 0) {
				tilePosition.y = 852;
				tilePosition.z = -10;
			}
			else {
				tilePosition.y = 840;
				tilePosition.z = 0;
			}

			GameObject tileObject = Instantiate(back, tilePosition, Quaternion.Euler(0, 0, 180));
			tileObject.transform.parent = gameObject.transform;
			allBacks.Add (tileObject);

		}

		// generate the wall in the right
		for (int i = 0; i < 36; i++) {

			Vector3 tilePosition;
			tilePosition.y = 700 - (i/2) * 82;

			if (i % 2 == 0) {
				tilePosition.x = 852;
				tilePosition.z = -10;
			}
			else {
				tilePosition.x = 840;
				tilePosition.z = 0;
			}

			GameObject tileObject = Instantiate(back, tilePosition, Quaternion.Euler(0, 0, 90));
			tileObject.transform.parent = gameObject.transform;
			allBacks.Add (tileObject);

		}

		// generate the wall in the bottom
		for (int i = 0; i < 36; i++) {

			Vector3 tilePosition;
			tilePosition.x = 700 - (i/2) * 82;

			if (i % 2 == 0) {
				tilePosition.y = -852;
				tilePosition.z = -10;
			}
			else {
				tilePosition.y = -840;
				tilePosition.z = 0;
			}

			GameObject tileObject = Instantiate(back, tilePosition, Quaternion.Euler(0, 0, 0));
			tileObject.transform.parent = gameObject.transform;
			allBacks.Add (tileObject);

		}

		// generate the wall in the left
		for (int i = 0; i < 36; i++) {

			Vector3 tilePosition;
			tilePosition.y = -700 + (i/2) * 82;

			if (i % 2 == 0) {
				tilePosition.x = -852;
				tilePosition.z = -10;
			}
			else {
				tilePosition.x = -840;
				tilePosition.z = 0;
			}

			GameObject tileObject = Instantiate(back, tilePosition, Quaternion.Euler(0, 0, -90));
			tileObject.transform.parent = gameObject.transform;
			allBacks.Add (tileObject);

		}
	}

	// Use this for initialization
	void Start () {

		Initialize ();
		InitializeFront ();
	}


	// Update is called once per frame
	void Update () {

	}
}
