using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	//
	public float timer = -1f;
	public Text timerText = null;

	bool gameOver = false;

	void Update()
	{
		if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
		{
			Win();
		}

		if (timer != -1f)
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
