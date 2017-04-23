using UnityEngine;
using System.Collections;

public class GravityMass : MonoBehaviour {

	Rigidbody2D rb;

	float gravityConstant = 9.8f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		ApplyGravity();
	}

    void ApplyGravity()
    {
        // accelerate it towards the planet. that's it.
        // planet's pos is ALWAYS 0,0
        //print( "Adding gravity! ");
        Vector2 pos = transform.position;
        //print( string.Format("Pos: {0}", pos) );
        Vector2 force = gravityConstant * pos * rb.mass * -1 / Mathf.Pow(pos.magnitude, 2);
        //print( string.Format("Force: {0}", force) );
        rb.AddForce( force );


    }
}
