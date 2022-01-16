using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	/// <summary>
	/// All the menus in the scene.
	/// </summary>
	public GameObject[] menus;

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
}
