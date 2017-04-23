using UnityEngine;
using System.Collections;

public class ShakyText : MonoBehaviour {

	public float maxOffset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShakeText(float magnitude) { // 0 min, 1 max

		magnitude = Mathf.Min(1, magnitude);
		magnitude = Mathf.Max(0, magnitude);

		float x = Random.Range(maxOffset * -1, maxOffset) * magnitude;
		float y = Random.Range(maxOffset * -1, maxOffset) * magnitude;

		transform.localPosition = new Vector3(x, y, 0);
	}
}
