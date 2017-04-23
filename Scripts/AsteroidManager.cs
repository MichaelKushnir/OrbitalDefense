using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AsteroidManager : MonoBehaviour {

	public float maxSpin;

	public Sprite big1, big2, big3, big4, big5, big6, big7, big8;
	public Sprite med1, med2, med3, med4;
	public Sprite small1, small2, small3, small4;
	public Sprite tiny1, tiny2, tiny3, tiny4;

	List<Sprite> bigSprites = new List<Sprite>();
	List<Sprite> medSprites = new List<Sprite>();
	List<Sprite> smallSprites = new List<Sprite>();
	List<Sprite> tinySprites = new List<Sprite>();

	float minCoord = 40f, maxCoord = 70f;
	float maxVelocity = 2;

	float asteroidTimer = 0f;
	float asteroidInterval = 10f;

	Transform asteroids;

	// Use this for initialization
	void Start () {
		asteroids = GameObject.Find("Asteroids").transform;

		bigSprites.Add(big1);
		bigSprites.Add(big2);
		bigSprites.Add(big3);
		bigSprites.Add(big4);
		bigSprites.Add(big5);
		bigSprites.Add(big6);
		bigSprites.Add(big7);
		bigSprites.Add(big8);

		medSprites.Add(med1);
		medSprites.Add(med2);
		medSprites.Add(med3);
		medSprites.Add(med4);

		smallSprites.Add(small1);
		smallSprites.Add(small2);
		smallSprites.Add(small3);
		smallSprites.Add(small4);

		tinySprites.Add(tiny1);
		tinySprites.Add(tiny2);
		tinySprites.Add(tiny3);
		tinySprites.Add(tiny4);
	}

	public void Setup() {
		asteroidTimer = 0f;

		asteroids = GameObject.Find("Asteroids").transform;

		List<GameObject> children = new List<GameObject>();
		foreach (Transform child in asteroids) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
	}
	
	// Update is called once per frame
	void Update () {
		asteroidTimer += Time.deltaTime;
		AsteroidCheck();
	}

	void AsteroidCheck() {
		if (asteroidTimer > asteroidInterval) {
			SpawnAsteroid();
			asteroidTimer = 0;
		}
	}

	public void SpawnAsteroid() {
		GameObject asteroidGO = Instantiate(Resources.Load("Asteroid", typeof(GameObject))) as GameObject;
		Asteroid asteroid = asteroidGO.GetComponent<Asteroid>();
		
		List<Sprite> sprites;
		int size = Random.Range(1, 4);
		switch (size) {
			case 1:
				sprites = tinySprites;
				asteroid.health = 1;
				asteroid.score = 1;
				asteroid.damage = 10;
				asteroid.powerupChance = .1f;
				asteroidGO.GetComponent<CircleCollider2D>().radius = .05f;
				asteroidGO.GetComponent<Rigidbody2D>().mass = 2f;
				break;
			case 2:
				sprites = smallSprites;
				asteroid.health = 1;
				asteroid.score = 3;
				asteroid.damage = 50;
				asteroid.powerupChance = .2f;
				asteroidGO.GetComponent<CircleCollider2D>().radius = .1f;
				asteroidGO.GetComponent<Rigidbody2D>().mass = 8f;
				break;
			case 3:
				sprites = medSprites;
				asteroid.health = 2;
				asteroid.score = 6;
				asteroid.damage = 100;
				asteroid.powerupChance = .4f;
				asteroidGO.GetComponent<CircleCollider2D>().radius = .2f;
				asteroidGO.GetComponent<Rigidbody2D>().mass = 30f;
				break;
			case 4:
				sprites = bigSprites;
				asteroid.health = 3;
				asteroid.score = 10;
				asteroid.damage = 200;
				asteroid.powerupChance = .7f;
				asteroidGO.GetComponent<CircleCollider2D>().radius = .6f;
				asteroidGO.GetComponent<Rigidbody2D>().mass = 100f;
				break;
			default:
				sprites = new List<Sprite>();
				break;
		}

		asteroid.size = size;
		int index = Random.Range(0, sprites.Count - 1);
		Sprite sprite = sprites[index];
		asteroidGO.GetComponent<SpriteRenderer>().sprite = sprite;
		asteroidGO.transform.Find("MinimapSprite").GetComponent<SpriteRenderer>().sprite = sprite;


		asteroidGO.transform.position = getSpawnCoords();
		print( asteroidGO.transform.position );
		asteroidGO.GetComponent<Rigidbody2D>().velocity = getSpawnVelocity();
		asteroidGO.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(maxSpin * -1, maxSpin);
		asteroidGO.transform.SetParent(asteroids);

		if (asteroidInterval > 5f)
			asteroidInterval *= .99f;
	}

	Vector3 getSpawnCoords() {
		float x = UnityEngine.Random.Range(minCoord, maxCoord);
		if (UnityEngine.Random.value > 0.5)
			x *= -1;
		float y = UnityEngine.Random.Range(minCoord, maxCoord);
		if (UnityEngine.Random.value > 0.5)
			y *= -1;

		return new Vector3(x, y, 0);
	}

	Vector3 getSpawnVelocity() {
		return UnityEngine.Random.insideUnitCircle * maxVelocity;
	}
}
