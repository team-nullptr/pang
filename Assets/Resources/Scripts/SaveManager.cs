using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

using LoadFunction = UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene, UnityEngine.SceneManagement.LoadSceneMode>;

public static class SaveManager
{
	const string SaveDirectory = "saves/";
	public const string Version = "1.0";

	static List<LoadFunction> loadFunctions = new List<LoadFunction>();

	static string SerializeStream(MemoryStream stream) {
		return System.Convert.ToBase64String(stream.ToArray());
	}

	/// <summary>
	/// Save current progress to a file.
	/// </summary>
	/// <param name="filename">The name of the save file.</param>
    public static void Save(string filename)
	{
		// Pause the game.
		GameController gameController = GameObject.FindObjectsOfType<GameController>()[0];
		gameController.Pause();

		// Open the save file.
		string path = GetSaveDirectory();

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

		Saveable[] saveables = GameObject.FindObjectsOfType<Saveable>();

		foreach (Saveable saveable in saveables)
		{
			// Save the savable's type.
			string type = saveable.GetType().AssemblyQualifiedName;

			fileStream.WriteLine(type);

			// Save the savable's data.
			MemoryStream dataStream = saveable.Save();

			fileStream.WriteLine(SerializeStream(dataStream));
		}

		fileStream.Close();
	}
	
	/// <summary>
	/// Loads the save data from a file.
	/// </summary>
	/// <param name="filename">The name of the save file.</param>
	/// <returns>True if the load was successful, false otherwise.</returns>
	///
	public static bool Load(string filename) {
		// Load the save file.
		string path = GetSaveDirectory() + filename;

		if (!File.Exists(path))
		{
			Debug.LogError("Save file not found: " + path);
			return false;
		}

		BinaryFormatter binaryFormatter = GetFormatter();

		StreamReader fileStream = new StreamReader(path);

		// Read the version.
		string version = fileStream.ReadLine();

		if (version != Version)
		{
			Debug.LogError("Save file version mismatch: " + version + " != " + Version);
			return false;
		}

		// Load the current level.
		string levelName = fileStream.ReadLine();
		SceneManager.LoadScene(levelName);

		// Load the current total score.
		int totalScore = int.Parse(fileStream.ReadLine());
		PointsManager.TotalScore = totalScore;

		// Call the OnLoad event.
		LoadFunction onLoad = (Scene scene, LoadSceneMode mode) => CallOnLoad();
		loadFunctions.Add(onLoad);
		SceneManager.sceneLoaded += onLoad;

		// Load the data from objects on the scene.
		GameObject dummyObject = new GameObject();

		while(fileStream.Peek() != -1)
		{
			// Load the savable's type.
			string type = fileStream.ReadLine();

			Saveable saveable = (Saveable)dummyObject.AddComponent(System.Type.GetType(type));

			// Load the savable's data.
			string data = fileStream.ReadLine();

			MemoryStream dataStream = new MemoryStream(System.Convert.FromBase64String(data));

			LoadFunction loadFunction = (Scene scene, LoadSceneMode mode) => saveable.Load(dataStream);
			loadFunctions.Add(loadFunction);
			SceneManager.sceneLoaded += loadFunction;
		}

		// Destroy the dummy object.
		GameObject.Destroy(dummyObject);

		// After loading all the objects into a new scene, clear the SceneManager's sceneLoaded event.
		LoadFunction clearLoadFunction = (Scene scene, LoadSceneMode mode) => ClearLoadFunctions();
		loadFunctions.Add(clearLoadFunction);
		SceneManager.sceneLoaded += clearLoadFunction;

		fileStream.Close();

		return true;
	}

	static void ClearLoadFunctions() {
		foreach (LoadFunction loadFunction in loadFunctions)
		{
			SceneManager.sceneLoaded -= loadFunction;
		}

		loadFunctions.Clear();
	}

	/// <summary>
	/// Calls the on load event for all savable objects.
	/// </summary>
	static void CallOnLoad() {
		Saveable[] saveables = GameObject.FindObjectsOfType<Saveable>();

		foreach (Saveable saveable in saveables)
		{
			saveable.OnLoad();
		}
	}
	
	/// <summary>
	/// Gets the formatter used for saving progress.
	/// </summary>
	public static BinaryFormatter GetFormatter() {
		BinaryFormatter formatter = new BinaryFormatter();

		return formatter;
	}

	/// <summary>
	/// Returns the directory where save files are stored.
	/// </summary>
	public static string GetSaveDirectory() {
		return Application.persistentDataPath + "/" + SaveDirectory;
	}
}
