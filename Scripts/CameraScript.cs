using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	GameObject player;

	public int zoomInSize, zoomOutSize;
	public float zoomSpeed = .2f;
	bool zoomedOut;

	Camera cam;
	MinimapCamera minimapCamera;

	// Use this for initialization
	void Start () {
		if (player == null) {
			player = GameObject.FindWithTag("Player");
			if (player == null)
				print("CameraScript could not find Player GameObject");
		}

		zoomedOut = false;
		cam = GetComponent<Camera>();
		minimapCamera = GameObject.Find("MinimapCamera").GetComponent<MinimapCamera>();

	}
	
	// Update is called once per frame
	void Update () {
		FollowPlayer();
		CheckScroll();
		AdjustZoom();
	}

	void FollowPlayer() {
		this.transform.position = player.transform.position + new Vector3(0, 0, -10);
	}

	void CheckScroll() {
		if (Input.GetAxis("Mouse ScrollWheel") > 0f ) { // forward
			zoomedOut = false;
			minimapCamera.SetZoomedOut(zoomedOut);
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) { // backwards
			zoomedOut = true;
			minimapCamera.SetZoomedOut(zoomedOut);
		}
	}

	void AdjustZoom() {
		if (zoomedOut)
			cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, zoomSpeed);
		else
			cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomInSize, zoomSpeed);
	}
}