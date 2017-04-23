using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour {

	GameObject player;
	float baseSpeed = 3f;

	PlayerShip.Stats stat;
	public PlayerShip.Stats GetStat() { return stat; }
	public void SetStat(PlayerShip.Stats s) { stat = s; }

	public int magnitude;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		ChasePlayer();
	}

	void ChasePlayer() {
		transform.position += (player.transform.position - transform.position).normalized * GetSpeed() * Time.deltaTime;
	}

	float GetSpeed() {
		return baseSpeed + 8 / (player.transform.position - transform.position).magnitude;
	}

	public void GetConsumed() {

		// spawn floaty text
		GameObject powerupTextGO = Instantiate(Resources.Load("PowerupFloatText", typeof(GameObject))) as GameObject;
		PowerupFloatText powerupText = powerupTextGO.GetComponent<PowerupFloatText>();
		powerupTextGO.GetComponent<RectTransform>().position = transform.position;
		powerupText.SetText(stat, magnitude);

		Object.Destroy(gameObject);
	}
}
