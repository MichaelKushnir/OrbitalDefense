using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public int health = 1;
	public int size = 1;
	public int damage = 10;
	public int score = 1;

	public float powerupChance = .5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(int damage) {
		
		health -= damage;

		if (health <= 0)
			Die();
		else
			GameObject.Find("AsteroidHitText").GetComponent<FlashText>().Flash("Asteroid hit");

	}

	void OnCollisionEnter2D(Collision2D coll) {
		Planet planet = coll.gameObject.GetComponent<Planet>();
        if (planet != null) {
        	planet.TakeDamage(damage);
        	BlowUp();
        	Object.Destroy(this.gameObject);
        }
        
    }

    void Die() {
		// report score to GameManager
		GameObject.FindWithTag("GameController").GetComponent<GameManager>().AddScore(score);
		GameObject.Find("AsteroidHitText").GetComponent<FlashText>().Flash("Asteroid destroyed");

		// see if we spawn a powerup
		if (Random.Range(0f, 1f) < powerupChance) {
			print( "Spawning powerup!" );
			GameObject.FindWithTag("GameController").GetComponent<PowerupManager>().SpawnPowerup(transform.position);
		}

		BlowUp();
    }

    void BlowUp() {

    	// make a boom boom
    	GameObject explosion = Instantiate(Resources.Load("Explosion", typeof(GameObject))) as GameObject;
    	explosion.transform.position = transform.position;
    	explosion.transform.localScale = transform.localScale;

    	// goodbye
		Object.Destroy(this.gameObject);
    }
}
