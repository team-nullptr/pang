using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	void Update()
	{
		if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
		{
			Win();
		}
	}

	public void Win()
	{
		Debug.Log("You win!");
	}

	public void Lose()
	{
		Debug.Log("You lose!");
	}
}
