using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public static float GetThrust() {
		return thrust;
	}

	Rigidbody2D rb;

	float rotationSpeed = 5f;
	float thrustMult = 5f;
	static float thrust = 0;

	PlayerShip playerShip;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		playerShip = GetComponent<PlayerShip>();
	}
	
	// Update is called once per frame
	void Update () {
		thrust = Input.mousePosition.y / Screen.height;
		ProcessInput();
	}

	void ProcessInput() {

		// Turning
		float torque = 0f;
		if (Input.GetButton("EnableMouseTurning"))
			torque = Input.GetAxis("Mouse X") * rotationSpeed * -1;
		else
			torque = Input.GetAxis("Horizontal") * rotationSpeed * -1;
		rb.AddTorque( torque );

		// Forward thrust
		if (Input.GetAxis("Vertical") > 0 && playerShip.GetFuel() > 0) {
			float thrust = GetThrust() * thrustMult;
			Vector3 force = rb.transform.up * thrust;
			rb.AddForce( force );
			playerShip.SetThrusting(1);
		} else if (Input.GetAxis("Vertical") < 0 && playerShip.GetFuel() > 0) {
			Vector3 force = rb.transform.up * thrustMult * -1;
			rb.AddForce( force );
			playerShip.SetThrusting(-1);
		} else
			playerShip.SetThrusting(0);

		// Shooting
		if (Input.GetButtonDown("Fire1"))
			playerShip.Fire();
		
	}
}
