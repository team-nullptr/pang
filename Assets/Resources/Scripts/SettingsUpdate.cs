using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUpdate : MonoBehaviour
{
    void Start()
    {
		// Update the volume according to the settings
		Settings.Update();
    }
}