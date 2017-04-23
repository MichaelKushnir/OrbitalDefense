using UnityEngine;
using System.Collections;

public class MenuStarManager : MonoBehaviour {

	int numStars = 1000;

	float maxCoord = 50f;

	float minSize = .3f, maxSize = 1.0f;

	public Sprite sprite1, sprite2, sprite3;

	int starsToMake = 0;

	public void QueueStar() { starsToMake += 1; }

	// Use this for initialization
	void Start () {
		SpawnStars(numStars);
	}
	
	// Update is called once per frame
	void Update () {
		SpawnStars(starsToMake);
		starsToMake = 0;
	}

	void SpawnStars(int num) {
		for (int ii = 0; ii < num; ii++) {
			SpawnStar();
		}

	}

	void SpawnStar() {
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

		star.transform.position = new Vector3(Random.Range(maxCoord * -1, maxCoord), Random.Range(maxCoord * -1, maxCoord), Random.Range(0, maxCoord));
		star.AddComponent<MenuStar>();
	}
}
