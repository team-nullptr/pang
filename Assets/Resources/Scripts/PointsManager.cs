using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
	public Text pointsText;

    int points;

	public int Points
	{
		get
		{
			return points;
		}
		set
		{
			points = value;
			pointsText.text = points.ToString();
		}
	}
}
