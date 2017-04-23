using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashText : MonoBehaviour {

	public float fadeStartTime;
	public float fadeSpeed;

	float timer = 0f;
	float flashFrequency = -1f; // set this to have the text auto-flash every this many seconds (low health, etc)
	public void SetFlashFrequency(float f) { flashFrequency = f; }

	public Color startColor, endColor;

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		Setup();
	}

	public void Setup() {
		text.color = endColor;
		fadeSpeed = 0.04f;
	}
	
	// Update is called once per frame
	void Update () {
		// fade
		if (timer > fadeStartTime) {
			if (text == null)
				text = GetComponent<Text>();
			text.color = Color.Lerp(text.color, endColor, fadeSpeed);
		}

		if (flashFrequency > 0f && timer > flashFrequency)
			Flash();

		timer += Time.deltaTime;

	}

	public void SetText(string str) {
		if (text == null)
			text = GetComponent<Text>();
		text.text = str;
	}

	public void Flash(string str) {
		SetText(str);
		Flash();
	}

	public void Flash() {
		text.color = startColor;
		timer = 0f;
	}

	public void FlashPermanently(string str) {
		Flash(str);
		fadeSpeed = 0f;
	}

}
