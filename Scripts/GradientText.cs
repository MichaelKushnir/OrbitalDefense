using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GradientText : MonoBehaviour {

	Text text;

	public Color minColor, maxColor;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.color = minColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetText(string str) { text.text = str; }

	public void SetColor(float gradient) {
		// 0 to 1
		text.color = Color.Lerp(minColor, maxColor, gradient);
	}
}
