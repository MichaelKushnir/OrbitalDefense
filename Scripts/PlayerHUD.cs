using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour {

	FlashText healthText, ammoText, fuelText;
	GradientText thrustGradientText;
	ShakyText thrustShakyText;
	Text altitudeText;

	int healthFlashThreshold = 20, ammoFlashThreshold = 50, fuelFlashThreshold = 20;

	PlayerShip player;

	// Use this for initialization
	void Start () {
		healthText = transform.Find("HealthText").GetComponent<FlashText>();
		ammoText   = transform.Find("AmmoText").GetComponent<FlashText>();
		fuelText   = transform.Find("FuelText").GetComponent<FlashText>();
		thrustGradientText = transform.Find("ThrustTextHolder").Find("ThrustText").GetComponent<GradientText>();
		thrustShakyText    = transform.Find("ThrustTextHolder").Find("ThrustText").GetComponent<ShakyText>();
		altitudeText = transform.Find("AltitudeText").GetComponent<Text>();

		player = GameObject.FindWithTag("Player").GetComponent<PlayerShip>();
		player.SetHUD(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateText(int health, int ammo, float fuel, float altitude) {
		
		healthText.SetText("Health: " + Mathf.Max(health, 0) + "<size=16> units</size>");
		if (health < healthFlashThreshold)
			healthText.SetFlashFrequency(1f);
		else
			healthText.SetFlashFrequency(-1f);

		ammoText.SetText("Ammo: " + ammo + "<size=16> lasers</size>");
		if (ammo < ammoFlashThreshold)
			ammoText.SetFlashFrequency(1f);
		else
			ammoText.SetFlashFrequency(-1f);

		fuelText.SetText("Fuel: " + Mathf.Max(fuel, 0f).ToString("0.00") + "<size=16> gallons</size>");
		if (fuel < fuelFlashThreshold)
			fuelText.SetFlashFrequency(1f);
		else
			fuelText.SetFlashFrequency(-1f);


		float thrust = PlayerMovement.GetThrust();
		string t = "Thrust: " + thrust.ToString("0.00") + "<size=16> Newton-Joules</size>\n";
		t += "Fuel burn rate: " + GameObject.FindWithTag("Player").GetComponent<PlayerShip>().GetFuelBurnRate().ToString("0.00") + "<size=16> gal/s</size>\n";
		thrustGradientText.SetText(t);
		thrustGradientText.SetColor(thrust * 2 - 1);

		if (player.GetThrusting() > 0)
			thrustShakyText.ShakeText(thrust * 2 - 1);
		else
			thrustShakyText.ShakeText(0);


		altitudeText.text = "Altitude: " + altitude.ToString("0.0") + "<size=16> furlongs</size>";

		
	}
}
