using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DisableIfSaveFileDoesntExist : MonoBehaviour
{
	/// <summary>
	/// The name of the save file that the button loads.
	/// </summary>
	public string slot = "slot1";

	Button button;
	string path;

    void Start()
    {
		// Get the button component.
        button = GetComponent<Button>();

		// Get the full path to the save file.
		path = SaveManager.GetSaveDirectory() + slot;
    }

	void Update() {
		// Make the button interactable only if the save file exists.
		button.interactable = File.Exists(path);
	}
}
