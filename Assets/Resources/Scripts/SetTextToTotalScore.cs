using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SetTextToTotalScore : MonoBehaviour
{
	Text text;

    void Start()
    {
		// Get the Text component.
        text = GetComponent<Text>();

		// Set the text to the total score.
		text.text = PointsManager.TotalScore.ToString();
    }
}
