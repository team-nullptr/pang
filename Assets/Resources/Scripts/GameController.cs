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

	bool gameOver = false, countdownFinished = false;

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

	void Start()
	{
		// Set the timer text to the timer value
		if (timerText != null)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
			timerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		}

		Pause();
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

		if (winMenu != null)
			winMenu.SetActive(true);
		Pause();

		gameOver = true;
	}

	public void Lose()
	{
		if (gameOver)
			return;

		if (loseMenu != null)
			loseMenu.SetActive(true);

		Pause();

		gameOver = true;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void NextLevel()
	{
		SceneManager.LoadScene(nextLevel);
	}

	public void PauseMenu() {
		if(gameOver)
			return;

		// If the game is paused, resume it
		if(GameState.paused) {
			Resume();

			if(pauseMenu != null)
				pauseMenu.SetActive(false);

			return;
		}

		if(pauseMenu == null)
			return;

		// If the game is not paused, pause it
		Pause();

		pauseMenu.SetActive(true);
	}
}
