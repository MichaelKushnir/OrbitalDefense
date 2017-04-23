using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartButton : MonoBehaviour {

	GameObject loadingText;
	AsyncOperation async = null;

	// Use this for initialization
	void Start () {
		loadingText = GameObject.Find("LoadingText");
		loadingText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (async != null) {
			loadingText.SetActive(true);
			loadingText.GetComponent<Text>().text = "Loading... (" + (int) (async.progress * 100) + "%)";
		}
	}

	public void LoadGame() {
		loadingText.SetActive(true);
		async = SceneManager.LoadSceneAsync("Main");

	}
}
