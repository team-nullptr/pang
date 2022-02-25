using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	/// <summary>
	/// The time after which the game ends and the player loses. Set to Mathf.Infinity to disable this.
	/// </summary>
	public float timer = Mathf.Infinity;
	/// <summary>
	/// The countdown before the level starts.
	/// </summary>
	public float countdown = 3f;
	public Text timerText = null;
	public Text countdownText = null;
	/// <summary>
	/// The level to be loaded after this level is won.
	/// </summary>
	public string nextLevel = "";
	/// <summary>
	/// The menu to be shown after the player wins.
	/// </summary>
	public GameObject winMenu;
	/// <summary>
	/// The menu to be shown after the player loses.
	/// </summary>
	public GameObject loseMenu;
	/// <summary>
	/// The menu to be shown after the player pauses.
	/// </summary>
	public GameObject pauseMenu;
	/// <summary>
	/// The level music.
	/// </summary>
	public AudioSource music;
	/// <summary>
	/// When time is lower than value, the music will be faster and timer will be red.
	/// </summary>
	public float warningTime = 10f;
	/// <summary>
	/// How faster the music will be when time is lower than warningTime.
	/// </summary>
	public float warningTimeMusicPitch = 1.2f;
	/// <summary>
	/// The color of the timer text when time is lower than warningTime.
	/// </summary>
	public Color warningTimeColor = Color.red;
	/// <summary>
	/// The text in win menu that displays score info.
	/// </summary>
	public Text scoreText;

	PointsManager pointsManager;
	PlayerManager playerManager;
	MenuManager menuManager;
	bool gameOver = false, countdownFinished = false, win = false;

	public void Pause()
	{
		Time.timeScale = 0;
		GameState.paused = true;
	}

	public void Resume()
	{
		Time.timeScale = 1;
		GameState.paused = false;
	}

	void Awake() {
		// Get the points manager
		pointsManager = GetComponent<PointsManager>();
	}

	void Start()
	{
		// Set the timer text to the timer value
		if (timerText != null)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
			timerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		}

		// Get the player manager
		GameObject player = GameObject.Find("Player");

		if (player != null)
			playerManager = player.GetComponent<PlayerManager>();

		// Get the menu manager
		GameObject menuManagerObject = GameObject.Find("MenuManager");
		if(menuManagerObject != null)
			menuManager = menuManagerObject.GetComponent<MenuManager>();

		// Start a countdown
		if(!gameOver)
			Pause();
		else {
			countdownFinished = true;
			countdownText.text = "";
			countdown = 0;
		}
	}

	void Update()
	{
		if (countdown > 0)
		{
			if (countdownText != null)
				countdownText.text = Mathf.Ceil(countdown).ToString();

			countdown -= Time.unscaledDeltaTime;

			return;
		}
		else if (!countdownFinished)
		{
			Resume();

			if (countdownText != null)
				countdownText.gameObject.SetActive(false);

			countdownFinished = true;
		}

		if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
		{
			Win();
		}

		if (timer != Mathf.Infinity)
		{
			timer -= Time.deltaTime;

			if (timer <= 0f)
			{
				timer = 0f;
				Lose();
			}

			if(timer <= warningTime) {
				music.pitch = warningTimeMusicPitch;
				timerText.color = warningTimeColor;
			}

			if (timerText != null)
			{
				TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
				timerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
			}
		}
	}

	public void Win()
	{
		if (gameOver)
			return;

		// Enable the win menu
		if (winMenu != null)
			menuManager.EnableMenu(winMenu);

		// Update the points info
		if(pointsManager != null) {
			// Count additional points for time and hp
			int pointsForTime = (int)timer * 100;
			int pointsForHp = 0;
			if(playerManager != null)
				pointsForHp = (playerManager.hp - 1) * 500;

			// Display the points information
			if(scoreText != null) {
				// I seriously don't want to do this like that, but it would be too much work to change it
				// TODO: Change it
				scoreText.text =
					"Score: " + pointsManager.Score.ToString() +
					"\nAdditional points for time: " + pointsForTime.ToString() +
					"\nAdditional points for HP: " + pointsForHp.ToString() +
					"\nTotal score: " + (PointsManager.TotalScore + pointsManager.Score + pointsForTime + pointsForHp).ToString();
			}
		}

		// Pause the game
		Pause();

		gameOver = true;
		win = true;
	}

	public void Lose()
	{
		if (gameOver)
			return;

		if (loseMenu != null)
			menuManager.EnableMenu(loseMenu);

		Pause();

		gameOver = true;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void AddScoreToTotal() {
		if(!win)
			return;

		if(pointsManager == null)
			return;
		
		// Count additional points for time and hp
		int pointsForTime = (int)timer * 100;
		int pointsForHp = 0;
		if(playerManager != null)
			pointsForHp = (playerManager.hp - 1) * 500;

		PointsManager.TotalScore += pointsManager.Score + pointsForTime + pointsForHp;
	}

	public void NextLevel()
	{
		if(!win)
			return;
		
		AddScoreToTotal();

		SceneManager.LoadScene(nextLevel);
	}

	public void PauseMenu() {
		// If the game is over or during the countdown, do nothing
		if(gameOver || countdown > 0)
			return;

		// If the game is paused, resume it
		if(GameState.paused) {
			Resume();

			menuManager.CloseAllMenus();

			return;
		}

		if(pauseMenu == null)
			return;

		// If the game is not paused, pause it
		Pause();

		menuManager.EnableMenu(pauseMenu);
	}

	public enum GameResult
	{
		InProgress,
		Won,
		Lost
	}

	/// <summary>
	/// Shows whether the game is won, lost or in progress.
	/// </summary>
	/// <returns>The result of the game.</returns>
	public GameResult GetGameResult() {
		if(gameOver) {
			if(win)
				return GameResult.Won;
			else
				return GameResult.Lost;
		}

		return GameResult.InProgress;
	}
}
