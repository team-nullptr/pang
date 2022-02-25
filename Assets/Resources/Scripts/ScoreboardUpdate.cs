using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardUpdate : MonoBehaviour
{
	/// <summary>
	/// The name of the file in which the scoreboard data is stored.
	/// </summary>
	public string scoreboardFilename = "scoreboard.save";

    void Start()
    {
		// Load the scoreboard data
		Scoreboard scoreboard = Scoreboard.Load(scoreboardFilename);

		// If there is no scoreboard yet, create one and save it
		if(scoreboard == null) {
			scoreboard = Scoreboard.DefaultScoreboard();

			scoreboard.Save(scoreboardFilename);
		}
		
        // Display the scoreboard
		for(int rowNum = 0; rowNum < scoreboard.rows.Length; rowNum++) {
			// Get the necessary handles
			Transform row = transform.GetChild(rowNum);
			Text rankObject = row.GetChild(0).gameObject.GetComponent<Text>();
			Text nameObject = row.GetChild(1).gameObject.GetComponent<Text>();
			Text scoreObject = row.GetChild(2).gameObject.GetComponent<Text>();

			// Display the rank
			rankObject.text = (rowNum + 1).ToString();

			// Display the name
			nameObject.text = scoreboard.rows[rowNum].name;

			// Display the score
			scoreObject.text = scoreboard.rows[rowNum].score.ToString();
		}
    }
}
