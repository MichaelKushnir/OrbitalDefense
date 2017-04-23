using UnityEngine;
using System.Collections;

public class PowerupFloatText : MonoBehaviour {

	float floatSpeed = .015f;
	float fadeStartTime = .3f;
	float fadeSpeed = .07f;
	float killTime = 2f;

	float timer = 0f;

	TextMesh textMesh;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// float
		transform.position += new Vector3(0f, floatSpeed, 0f);

		// fade
		if (timer > fadeStartTime)
			textMesh.color = Color.Lerp(textMesh.color, Color.clear, fadeSpeed);
		
		// die 
		if (timer > killTime)
			Object.Destroy(gameObject);


		timer += Time.deltaTime;
	}

	public void SetText(PlayerShip.Stats stat, int magnitude) {

		if (textMesh == null)
			textMesh = GetComponent<TextMesh>();

		textMesh.text = "+" + magnitude;

		switch (stat) {
			case PlayerShip.Stats.HEALTH:
				textMesh.text += " Health";
				break;
			case PlayerShip.Stats.AMMO:
				textMesh.text += " Ammo";
				break;
			case PlayerShip.Stats.FUEL:
				textMesh.text += " Fuel";
				break;
			default:
				textMesh.text += " ???";
				break;
		}
	}

}
