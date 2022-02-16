using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class Scoreboard
{
	[System.Serializable]
	public struct ScoreboardRow {
		public string name;
		public int score;

		public ScoreboardRow(string name, int score) {
			this.name = name;
			this.score = score;
		}
	}

	public ScoreboardRow[] rows;

	public Scoreboard() {
		rows = new ScoreboardRow[10];
	}

	public void Save(string file = "scoreboard.save") {
		string path = Application.persistentDataPath + "/" + file;

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
		formatter.Serialize(fileStream, this);
		fileStream.Close();
	}

	public static Scoreboard Load(string file = "scoreboard.save") {
		string path = Application.persistentDataPath + "/" + file;

		if(!File.Exists(path)) {
			return null;
		}

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open);
		Scoreboard result = (Scoreboard)formatter.Deserialize(fileStream);
		fileStream.Close();

		return result;
	}

	public static Scoreboard DefaultScoreboard() {
		Scoreboard result = new Scoreboard();

		// Fill the scoreboard with default data
		result.rows[0] = new ScoreboardRow("John Pang", 100000);
		result.rows[1] = new ScoreboardRow("John Pang", 90000);
		result.rows[2] = new ScoreboardRow("John Pang", 80000);
		result.rows[3] = new ScoreboardRow("John Pang", 70000);
		result.rows[4] = new ScoreboardRow("John Pang", 60000);
		result.rows[5] = new ScoreboardRow("John Pang", 50000);
		result.rows[6] = new ScoreboardRow("John Pang", 40000);
		result.rows[7] = new ScoreboardRow("John Pang", 30000);
		result.rows[8] = new ScoreboardRow("John Pang", 20000);
		result.rows[9] = new ScoreboardRow("John Pang", 10000);

		return result;
	}
}
