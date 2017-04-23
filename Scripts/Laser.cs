using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	int bound = 10000;

	int damage = 1;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		CheckBound();
		CheckSpeed();
	}

	void CheckBound() {
		// if the laser goes out of bounds, destroy it
		if (Mathf.Max(Mathf.Abs(transform.position.x), Mathf.Abs(transform.position.y)) > bound) {
			Object.Destroy(this.gameObject);
		}
	}

	void CheckSpeed() {
		if (rb == null)
			rb = GetComponent<Rigidbody2D>();

		if (rb.velocity.magnitude < 3 && (transform.position - GameObject.FindWithTag("Player").transform.position).magnitude > 5) {
			print("Destroying laser because it's too damn slow");
			Object.Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Asteroid asteroid = coll.gameObject.GetComponent<Asteroid>();
        if (asteroid != null) {

        	asteroid.TakeDamage(damage);

        	Object.Destroy(this.gameObject);
        } else if (coll.gameObject.GetComponent<Planet>() != null) {
        	// uh oh you hit the planet you dingus
        	coll.gameObject.GetComponent<Planet>().TakeDamage(1);
        	Object.Destroy(this.gameObject);
        }
    }
}
