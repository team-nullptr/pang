using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameControllerSaveable : Saveable
{
	[System.Serializable]
	struct GameControllerData {
		public int score, totalScore;
		public float timer;
		public GameController.GameResult result;
	}
	
    public override MemoryStream Save()
	{
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		MemoryStream memoryStream = new MemoryStream();

		// Get necessary data.
		GameControllerData data = new GameControllerData();
		GameController gameController = GetComponent<GameController>();
		PointsManager pointsManager = GetComponent<PointsManager>();
		data.totalScore = PointsManager.TotalScore;
		data.score = pointsManager.Score;
		data.timer = gameController.timer;
		data.result = gameController.GetGameResult();

		// Serialize the data.
		binaryFormatter.Serialize(memoryStream, data);
		return memoryStream;
	}

	public override void Load(MemoryStream saveData)
	{
		// Find the game controller object.
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");

		// Deserialize the data.
		BinaryFormatter binaryFormatter = SaveManager.GetFormatter();
		saveData.Position = 0;
		GameControllerData data = (GameControllerData)binaryFormatter.Deserialize(saveData);

		// Load the data.
		PointsManager.TotalScore = data.totalScore;
		GameController gameController = gameControllerObject.GetComponent<GameController>();
		PointsManager pointsManager = gameControllerObject.GetComponent<PointsManager>();
		pointsManager.Score = data.score;
		gameController.timer = data.timer;

		// Update the game result.
		switch (data.result) {
			case GameController.GameResult.InProgress:
				gameController.Resume();
				break;

			case GameController.GameResult.Won:
				gameController.Win();
				break;

			case GameController.GameResult.Lost:
				gameController.Lose();
				GameObject player = GameObject.FindWithTag("Player");
				Destroy(player);
				break;
		}
	}

	public override void OnLoad() {}
}
