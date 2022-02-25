using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	/// <summary>
	/// The menu that is currently active.
	/// </summary>
	public GameObject currentMenu;

	public void EnableMenu(GameObject menu)
	{
		if(currentMenu != null)
			currentMenu.SetActive(false);
		
		menu.SetActive(true);

		currentMenu = menu;
	}

	public void CloseAllMenus() {
		currentMenu.SetActive(false);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void LoadLevel(string levelName)
	{
		SceneManager.LoadScene(levelName);
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
