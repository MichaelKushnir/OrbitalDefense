using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	int score = 0;
	FlashText scoreText;

	GameObject player;
	GameObject planet;
	GameObject restartButton;


	// Use this for initialization
	void Start () {
		UpdateScoreText();

		player = GameObject.FindWithTag("Player");
		planet = GameObject.Find("PlanetSprite");
		restartButton = GameObject.Find("RestartButton");

		SetupGame();
	}

	public void SetupGame() {
		score = 0;
		UpdateScoreText();

		player.SetActive(true);
		player.GetComponent<PlayerShip>().Setup();

		planet.SetActive(true);
		planet.GetComponent<Planet>().Setup();

		GetComponent<AsteroidManager>().Setup();

		print("setting up game");
		restartButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void AddScore(int points) {
		score += points;
		UpdateScoreText();
	}

	void UpdateScoreText() {
		if (scoreText == null)
			scoreText = GameObject.Find("ScoreHUD").GetComponent<FlashText>();
			
		scoreText.SetText("Score: " + score);
		if (score > 0)
			scoreText.Flash();
	}

	public void GameOver() {
		restartButton.SetActive(true);
	}

}
