using UnityEngine;
using System.Collections;

public class StarManager : MonoBehaviour {

	int numStars = 15000;

	float maxCoord = 500f;

	float minSize = .3f, maxSize = 1.0f;

	public Sprite sprite1, sprite2, sprite3;

	Transform stars;

	// Use this for initialization
	void Start () {
		stars = GameObject.Find("Stars").transform;
		SpawnStars();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnStars() {
		for (int ii = 0; ii < numStars; ii++)
			SpawnStar();
	}

	GameObject SpawnStar() {
		GameObject star = Instantiate(Resources.Load("Star", typeof(GameObject))) as GameObject;

		// choose 1 of 3 sprites
		int rand = Random.Range(1, 3);
		if (rand == 1)
			star.GetComponent<SpriteRenderer>().sprite = sprite1;
		else if (rand == 2)
			star.GetComponent<SpriteRenderer>().sprite = sprite2;
		else
			star.GetComponent<SpriteRenderer>().sprite = sprite3;

		star.transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)));

		float size = Random.Range(minSize, maxSize);
		star.transform.localScale *= size;

		star.transform.SetParent(stars);
		star.transform.position = new Vector3(GetScaledCoord(), GetScaledCoord(), 10);

		return star;
	}

	float GetScaledCoord() {
		return Random.Range(0, maxCoord) - Random.Range(0, maxCoord);
	}
}
