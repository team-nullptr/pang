using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeSlider : MonoBehaviour
{
	Slider slider;

	void Start()
	{
		slider = GetComponent<Slider>();
		slider.value = Settings.SfxVolume;
	}
}
