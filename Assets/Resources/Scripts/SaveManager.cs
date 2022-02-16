using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManager
{
	public const string SaveDirectory = "saves/";
	public const string Version = "1.0";

	static string SerializeStream(MemoryStream stream) {
		return System.Convert.ToBase64String(stream.ToArray());
	}

	/// <summary>
	/// Save current progress to a file.
	/// </summary>
	/// <param name="filename">The name of the save file.</param>
    public static void Save(string filename)
	{
		// Open the save file.
		string path = Application.persistentDataPath + "/" + SaveDirectory;

		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

		path += filename;

		BinaryFormatter binaryFormatter = GetFormatter();

		StreamWriter fileStream = new StreamWriter(path);

		// Write the version.
		fileStream.WriteLine(Version);

		// Save the current level.
		string levelName = SceneManager.GetActiveScene().name;

		fileStream.WriteLine(levelName);

		// Save the current total score.
		int totalScore = PointsManager.TotalScore;

		fileStream.WriteLine(totalScore);

		// Save the data from objects on the scene.

		Savable[] savables = GameObject.FindObjectsOfType<Savable>();

		foreach (Savable savable in savables)
		{
			// Save the savable's type.
			string type = savable.GetType().AssemblyQualifiedName;

			fileStream.WriteLine(type);

			// Save the savable's data.
			MemoryStream dataStream = savable.Save();

			fileStream.WriteLine(SerializeStream(dataStream));
		}

		fileStream.Close();
	}
	
	/// <summary>
	/// Loads the save data from a file.
	/// </summary>
	/// <param name="filename">The name of the save file.</param>
	public static void Load(string filename) {
		// Load the save file.
		string path = Application.persistentDataPath + "/" + SaveDirectory + filename;

		if (!File.Exists(path))
		{
			Debug.LogError("Save file not found: " + path);
			return;
		}

		BinaryFormatter binaryFormatter = GetFormatter();

		StreamReader fileStream = new StreamReader(path);

		// Read the version.
		string version = fileStream.ReadLine();

		if (version != Version)
		{
			Debug.LogError("Save file version mismatch: " + version + " != " + Version);
			return;
		}

		// Load the current level.
		string levelName = fileStream.ReadLine();
		SceneManager.LoadScene(levelName);

		// Load the current total score.
		int totalScore = int.Parse(fileStream.ReadLine());
		PointsManager.TotalScore = totalScore;

		// Load the data from objects on the scene.
		GameObject dummyObject = new GameObject();

		while(fileStream.Peek() != -1)
		{
			// Load the savable's type.
			string type = fileStream.ReadLine();

			Savable savable = (Savable)dummyObject.AddComponent(System.Type.GetType(type));

			// Load the savable's data.
			string data = fileStream.ReadLine();

			MemoryStream dataStream = new MemoryStream(System.Convert.FromBase64String(data));

			SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => savable.Load(dataStream);
		}

		fileStream.Close();
	}
	
	/// <summary>
	/// Gets the formatter used for saving progress.
	/// </summary>
	public static BinaryFormatter GetFormatter() {
		BinaryFormatter formatter = new BinaryFormatter();

		return formatter;
	}
}
