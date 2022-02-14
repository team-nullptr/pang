using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Scoreboard
{
	[System.Serializable]
	public struct ScoreboardRow {
		string name;
		int points;
	}

	ScoreboardRow[] rows;

	Scoreboard() {
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
			Debug.LogError("Scoreboard file \"" + path + "\" not found when trying to read.");
			return null;
		}

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open);
		Scoreboard result = (Scoreboard)formatter.Deserialize(fileStream);
		fileStream.Close();

		return result;
	}
}
