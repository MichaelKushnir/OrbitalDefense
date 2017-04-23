using UnityEngine;
using System.Collections;

public class MenuStar : MonoBehaviour {

	public float velocity = -10f;
	MenuStarManager manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find("MenuStarManager").GetComponent<MenuStarManager>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(0, 0, velocity) * Time.deltaTime;

		if (transform.position.z < -15) {
			manager.QueueStar();
			Object.Destroy(gameObject);
		}
	}
}
