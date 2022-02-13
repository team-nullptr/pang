using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
	public Text pointsText;

    int score;

	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
			pointsText.text = score.ToString();
		}
	}

	public static int TotalScore = 0;

	public void ResetTotalScore() {
		TotalScore = 0;
	}
}
