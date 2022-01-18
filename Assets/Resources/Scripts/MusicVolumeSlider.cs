using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
	Slider slider;

	void Start()
	{
		slider = GetComponent<Slider>();
		slider.value = Settings.MusicVolume;
	}
}
