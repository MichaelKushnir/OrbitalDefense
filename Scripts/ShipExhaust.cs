using UnityEngine;
using System.Collections;

public class ShipExhaust : MonoBehaviour {

	Animator animator;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float thrust = PlayerMovement.GetThrust();
		audioSource.volume = thrust;
		audioSource.pitch = 0.8f + thrust * 0.4f;
		animator.speed = 0.4f + thrust * 1.2f;
		transform.localScale = new Vector3(1f + thrust * 2.5f, 1f + thrust * 2.5f, 1f);
		transform.localPosition = new Vector3(0f, -.5f - thrust * .4f, 1f);
	}
}
