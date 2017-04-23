using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShip : MonoBehaviour {

	float fireForce = 100f;

	public enum Stats { HEALTH, AMMO, FUEL };

	public int baseHealth, baseAmmo;
	public int baseFuel;

	int health;
	float damageScale = 5f;
	int ammo;
	float fuel;
	float fuelBurnExp = 2f; // controls curve of thrust vs. fuel efficiency

	public int GetHealth() { return health; }
	public int GetAmmo() { return ammo; }
	public float GetFuel() { return fuel; }

	PlayerHUD hud;
	public void SetHUD(PlayerHUD phud) {
		hud = phud;
	}

	GameObject shipExhaust;
	int isThrusting = 0; // -1 for backwards, 1 for forwards
	public int GetThrusting() { return isThrusting; }

	Transform lasers;
	float fireOffsetX = .5f, fireOffsetY = .2f;
	AudioSource audioSource;
	public AudioClip laserAudioClip, powerupAudioClip, crashAudioClip1, crashAudioClip2, crashAudioClip3;
	List<AudioClip> crashAudioClips = new List<AudioClip>();
	int crashAudioClipsIndex = 0;

	float deathTimer = 0f;
	bool deathTimerActive = false;

	// Use this for initialization
	void Start () {
		lasers = GameObject.Find("Lasers").transform;
		shipExhaust = GameObject.Find("ShipExhaust");
		audioSource = GetComponent<AudioSource>();

		crashAudioClips.Add(crashAudioClip1);
		crashAudioClips.Add(crashAudioClip2);
		crashAudioClips.Add(crashAudioClip3);

		Setup();
	}

	public void Setup() {
		health = baseHealth;
		ammo = baseAmmo;
		fuel = baseFuel;
		transform.position = new Vector3(0f, 10.16f, 0f);
		transform.rotation = Quaternion.identity;
		deathTimerActive = false;
	}
	
	// Update is called once per frame
	void Update () {
		hud.UpdateText(health, ammo, fuel, GetAltitude());
		if (health <= 0) { Die(); }
		Thrust();

		if (deathTimerActive) {
			deathTimer -= Time.deltaTime;

			if (deathTimer <= 0f)
				Die();
		}
	}

	float GetAltitude() { return transform.position.magnitude - 10; }

	public void SetThrusting( int i ) { isThrusting = i; }

	void Thrust() {
		if (isThrusting != 0)
			fuel -= GetFuelBurnRate() * Time.deltaTime;

		shipExhaust.SetActive(isThrusting > 0);
	}

	public float GetFuelBurnRate() {
		if (isThrusting > 0)
			return Mathf.Pow(PlayerMovement.GetThrust(), fuelBurnExp);
		else if (isThrusting < 0)
			return 1;
		else
			return 0;
	}

	public void Fire() {

		if (ammo > 0) {
			ammo -= 1;

			Vector3 firePos = transform.TransformPoint(new Vector3(fireOffsetX, fireOffsetY, 0));

			GameObject laserFlash = Instantiate(Resources.Load("LaserFlash", typeof(GameObject))) as GameObject;
			laserFlash.transform.localPosition = firePos;

			GameObject laser = Instantiate(Resources.Load("Laser", typeof(GameObject))) as GameObject;
			laser.transform.localPosition = firePos;
			laser.transform.rotation = transform.rotation;
			laser.GetComponent<Rigidbody2D>().AddForce(fireForce * transform.up);
			laser.transform.SetParent(lasers);

			audioSource.PlayOneShot(laserAudioClip);

			fireOffsetX *= -1;
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.GetComponent<Asteroid>() || coll.gameObject.GetComponent<Planet>()) {
			Vector2 force = GetComponent<Rigidbody2D>().velocity;
			Rigidbody2D rbOther = coll.gameObject.GetComponent<Rigidbody2D>();
			if (rbOther != null) {
				force += rbOther.velocity;
			}

			int damage = (int) (force.magnitude * damageScale);
			if (damage > 0)
				TakeDamage(damage);
			
		}
		else {
			Powerup powerup = coll.gameObject.GetComponent<Powerup>();
			if (powerup) {
				ApplyPowerup(powerup);
			}
		}
	}

	void TakeDamage(int damage) {
		health -= damage;
		GameObject.Find("HealthText").GetComponent<FlashText>().Flash();
		audioSource.PlayOneShot(crashAudioClips[crashAudioClipsIndex]);
		crashAudioClipsIndex = (crashAudioClipsIndex + 1) % crashAudioClips.Count;
	}

	void ApplyPowerup(Powerup powerup) {
		switch(powerup.GetStat()) {
			case Stats.HEALTH:
				health += powerup.magnitude;
				break;
			case Stats.AMMO:
				ammo += powerup.magnitude;
				break;
			case Stats.FUEL:
				fuel += powerup.magnitude;
				break;
			default:
				break;
		}

		audioSource.PlayOneShot(powerupAudioClip);

		powerup.GetConsumed();
	}

	void Die() {
    	// make a boom boom
    	GameObject explosion = Instantiate(Resources.Load("Explosion", typeof(GameObject))) as GameObject;
    	explosion.transform.position = transform.position;
       	explosion.transform.localScale = new Vector3(2, 2, 1);

		GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();

		gameObject.SetActive(false);
	}

	public void SetDeathTimer(float time) {
		deathTimer = time;
		deathTimerActive = true;
	}

	public void SelfDestruct() {
		Die();
	}

}
