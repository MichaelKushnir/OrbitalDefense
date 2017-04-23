using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Planet : MonoBehaviour {

	FlashText planetHudText, planetHitText;

	static readonly int baseHealth = 1000;

	int healthFlashThreshold = 200;

	int health = baseHealth;

	// Use this for initialization
	void Start () {
		planetHudText = GameObject.Find("PlanetHUD").GetComponent<FlashText>();
		planetHitText = GameObject.Find("PlanetHitText").GetComponent<FlashText>();
		UpdatePlanetHudText();

		Setup();
	}

	public void Setup() {
		health = baseHealth;
		UpdatePlanetHudText();
		planetHitText = GameObject.Find("PlanetHitText").GetComponent<FlashText>();
		planetHitText.fadeSpeed = 0.4f;
		planetHitText.Setup();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
			Die();
	}

	void UpdatePlanetHudText() {
		if (planetHudText == null)
			planetHudText = GameObject.Find("PlanetHUD").GetComponent<FlashText>();

		planetHudText.SetText("Planet Health: " + Mathf.Max(0, health));

		if (health < healthFlashThreshold)
			planetHudText.SetFlashFrequency(1f);
		else
			planetHudText.SetFlashFrequency(-1f);

	}

	public void TakeDamage(int damage) {
		health -= damage;
		UpdatePlanetHudText();
		planetHitText.Flash();
		planetHudText.Flash();
	}

	void Die() {

		// kill the player in 3 seconds
		GameObject.FindWithTag("Player").GetComponent<PlayerShip>().SetDeathTimer(3f);

    	// make a boom boom
    	GameObject explosion = Instantiate(Resources.Load("Explosion", typeof(GameObject))) as GameObject;
    	explosion.transform.position = transform.position;
       	explosion.transform.localScale = new Vector3(50, 50, 1);

       	planetHitText.FlashPermanently("Planet destroyed");

		gameObject.SetActive(false);
	}
}
