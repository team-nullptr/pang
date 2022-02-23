using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
	/// <summary>
	/// The name of the save file that the button loads.
	/// </summary>
	public string slot = "slot1";

	Button button;

    void Start()
    {
        button = GetComponent<Button>();
		string path = SaveManager.GetSaveDirectory() + slot;

		if (!File.Exists(path)) {
			button.interactable = false;
		}
    }
}
