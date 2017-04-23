using UnityEngine;
using System.Collections;

public class MinimapCamera : MonoBehaviour {

	Transform playerTransform;
	Transform playerMinimapSpriteTransform;
	Camera cam;

	public int zoomInSize, zoomOutSize;
	public float zoomSpeed = .2f;
	bool zoomedOut;
	public void SetZoomedOut(bool b) { zoomedOut = b; }

	public float playerMinimapSpriteZoomInMult, playerMinimapSpriteZoomOutMult;

	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		playerMinimapSpriteTransform = playerTransform.Find("MinimapSprite");
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		AdjustZoom();

		if (zoomedOut)
			PositionPlayerMinimapSprite();
	}

	void AdjustZoom() {
		if (zoomedOut) {
			cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, zoomSpeed);
			playerMinimapSpriteTransform.localScale = Vector3.Lerp(playerMinimapSpriteTransform.localScale,
				new Vector3(playerMinimapSpriteZoomOutMult, playerMinimapSpriteZoomOutMult, 1), zoomSpeed);

			transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, -10), zoomSpeed);
		}
		else {
			cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomInSize, zoomSpeed);
			playerMinimapSpriteTransform.localScale = Vector3.Lerp(playerMinimapSpriteTransform.localScale,
				new Vector3(playerMinimapSpriteZoomInMult, playerMinimapSpriteZoomInMult, 1), zoomSpeed);

			transform.position = Vector3.Lerp(transform.position, playerTransform.position + new Vector3(0, 0, -10), zoomSpeed);
		}
	}

	void PositionPlayerMinimapSprite() {
		if (playerTransform.position.magnitude > cam.orthographicSize * .95f)
			playerMinimapSpriteTransform.position = playerTransform.position * (cam.orthographicSize * 0.95f / playerTransform.position.magnitude);
		else
			playerMinimapSpriteTransform.localPosition = new Vector3(0, 0, 0);
	}
}
