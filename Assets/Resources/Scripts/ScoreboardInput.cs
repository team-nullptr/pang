using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardInput : MonoBehaviour
{
	/// <summary>
	/// The name of the file in which the scoreboard data is stored.
	/// </summary>
	public string scoreboardFilename = "scoreboard.save";
	/// <summary>
	/// The input box for asking player's name.
	/// </summary>
	public InputField NameInputPrefab;

	InputField nameInput;
	Scoreboard scoreboard;
	int playerRow;

    void Start()
    {
		// Load the scoreboard data
		scoreboard = Scoreboard.Load(scoreboardFilename);

		// If there is no scoreboard yet, create one
		if(scoreboard == null)
			scoreboard = Scoreboard.DefaultScoreboard();

		// A row in which the player's name and score will be stored (if he's not worthy, it's -1)
		playerRow = -1;

		// Find a row in which the player should be
		for(int rowNum = 0; rowNum < scoreboard.rows.Length; rowNum++) {
			if(scoreboard.rows[rowNum].score <= PointsManager.TotalScore) {
				playerRow = rowNum;
				break;
			}
		}

		// Move all the rows below the player's row down
		if(playerRow != -1) {
			for(int rowNum = scoreboard.rows.Length - 1; rowNum > playerRow; rowNum--) {
				scoreboard.rows[rowNum] = scoreboard.rows[rowNum - 1];
			}
		}

		// Save the player's score
		if(playerRow != -1)
			scoreboard.rows[playerRow].score = PointsManager.TotalScore;

        // Display the scoreboard, with the input box if the player should appear on it
		for(int rowNum = 0; rowNum < scoreboard.rows.Length; rowNum++) {
			// Get the necessary handles
			Transform row = transform.GetChild(rowNum);
			Text rankObject = row.GetChild(0).gameObject.GetComponent<Text>();
			Text nameObject = row.GetChild(1).gameObject.GetComponent<Text>();
			Text scoreObject = row.GetChild(2).gameObject.GetComponent<Text>();

			// Display the rank
			rankObject.text = (rowNum + 1).ToString();

			// Display the name (or input box if its the player's row)
			if(rowNum == playerRow) {
				// Clear the text
				nameObject.text = "";

				// Create the input box
				nameInput = Instantiate(NameInputPrefab, row.GetChild(1).position, Quaternion.identity);
				nameInput.gameObject.transform.SetParent(row.GetChild(1));
			}
			else
				nameObject.text = scoreboard.rows[rowNum].name;

			// Display the score
			scoreObject.text = scoreboard.rows[rowNum].score.ToString();
		}
    }

	public void Save() {
		// Save the player's name
		if(playerRow != -1)
			scoreboard.rows[playerRow].name = nameInput.text;

		// Save the scoreboard
		scoreboard.Save(scoreboardFilename);
	}
}
