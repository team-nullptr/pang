using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// TODO: Set default values of the volume sliders to the actual values from PlayerPrefs

public class MenuManager : MonoBehaviour
{
	/// <summary>
	/// All the menus in the scene.
	/// </summary>
	public GameObject[] menus;
	/// <summary>
	/// Easy levels.
	/// </summary>
	public Scene[] easyLevels;
	/// <summary>
	/// Medium levels.
	/// </summary>
	public Scene[] mediumLevels;
	/// <summary>
	/// Hard levels.
	/// </summary>
	public Scene[] hardLevels;

	public void EnableMenu(GameObject menu)
	{
		foreach (GameObject _menu in menus)
		{
			if (_menu == menu)
			{
				_menu.SetActive(true);
			}
			else
			{
				_menu.SetActive(false);
			}
		}
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void LoadLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}

	public void LoadLevel(Scene scene)
	{
		SceneManager.LoadScene(scene.name);
	}

	public void LoadRandomLevel(Scene[] scenes)
	{
		SceneManager.LoadScene(scenes[Random.Range(0, scenes.Length)].name);
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

	public void SetMusicVolume(float volume)
	{
		Settings.MusicVolume = volume;
	}

	public void SetSFXVolume(float volume)
	{
		Settings.SfxVolume = volume;
	}

	public void SetMusicVolumeFromSlider(Slider slider)
	{
		SetMusicVolume(slider.value);
	}

	public void SetSFXVolumeFromSlider(Slider slider)
	{
		SetSFXVolume(slider.value);
	}
}
