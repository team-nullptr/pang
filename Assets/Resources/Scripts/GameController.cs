using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	/// <summary>
	/// The time after which the game ends and the player loses. Set to Mathf.Infinity to disable this.
	/// </summary>
	public float timer = Mathf.Infinity;
	public float countdown = 3f;
	public Text timerText = null;
	public Text CountdownText = null;

	bool gameOver = false;

	public void Pause()
	{
		Time.timeScale = 0;
	}

	public void Resume()
	{
		Time.timeScale = 1;
	}

	void Start()
	{
		Settings.Update();

		Pause();
	}

	void Update()
	{
		if (countdown > 0)
		{
			if (CountdownText != null)
				CountdownText.text = Mathf.Ceil(countdown).ToString();

			countdown -= Time.unscaledDeltaTime;

			return;
		}
		else
		{
			Resume();

			if (CountdownText != null)
				CountdownText.gameObject.SetActive(false);
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

		Debug.Log("You win!");

		gameOver = true;
	}

	public void Lose()
	{
		if (gameOver)
			return;

		Debug.Log("You lose!");

		gameOver = true;
	}
}
