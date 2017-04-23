using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {

	public Sprite healthPowerupSprite, ammoPowerupSprite, fuelPowerupSprite;
	public AudioClip powerupSpawnAudioClip;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Powerup SpawnPowerup(Vector3 position) {
		GameObject powerupGO = Instantiate(Resources.Load("Powerup", typeof(GameObject))) as GameObject;
		powerupGO.transform.position = position;
		Powerup powerup = powerupGO.GetComponent<Powerup>();

		int rand = Random.Range(1,5);

		switch (rand) {
			case 1:
				print( "Spawning HEALTH powerup" );
				powerup.GetComponent<SpriteRenderer>().sprite = healthPowerupSprite;
				powerup.SetStat(PlayerShip.Stats.HEALTH);
				powerup.magnitude = 20;
				break;
			case 2:
				print( "Spawning AMMO powerup" );
				powerup.GetComponent<SpriteRenderer>().sprite = ammoPowerupSprite;
				powerup.SetStat(PlayerShip.Stats.AMMO);
				powerup.magnitude = 50;
				break;
			default:
				print( "Spawning FUEL powerup" );
				powerup.GetComponent<SpriteRenderer>().sprite = fuelPowerupSprite;
				powerup.SetStat(PlayerShip.Stats.FUEL);
				powerup.magnitude = 10;
				break;

		}

		audioSource.PlayOneShot(powerupSpawnAudioClip);
	
		return powerup;

	}

}
