using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Module1LevelManager : MonoBehaviour
{
	/// <summary>
	/// Easy levels.
	/// </summary>
	public string[] easyLevels;
	/// <summary>
	/// Medium levels.
	/// </summary>
	public string[] mediumLevels;
	/// <summary>
	/// Hard levels.
	/// </summary>
	public string[] hardLevels;

	public void LoadRandomLevel(string[] scenes)
	{
		SceneManager.LoadScene(scenes[Random.Range(0, scenes.Length)]);
	}

	public void LoadEasyLevel()
	{
		LoadRandomLevel(easyLevels);
	}

	public void LoadMediumLevel()
	{
		LoadRandomLevel(mediumLevels);
	}

	public void LoadHardLevel()
	{
		LoadRandomLevel(hardLevels);
	}
}
