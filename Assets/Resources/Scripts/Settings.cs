using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
	public static float MusicVolume
	{
		get
		{
			if (!PlayerPrefs.HasKey("MusicVolume"))
			{
				PlayerPrefs.SetFloat("MusicVolume", 1f);
			}

			return PlayerPrefs.GetFloat("MusicVolume", 1f);
		}
		set
		{
			PlayerPrefs.SetFloat("MusicVolume", value);

			UpdateMusicVolume();
		}
	}

	public static float SfxVolume
	{
		get
		{
			if (!PlayerPrefs.HasKey("SfxVolume"))
			{
				PlayerPrefs.SetFloat("SfxVolume", 1f);
			}

			return PlayerPrefs.GetFloat("SfxVolume", 1f);
		}
		set
		{
			PlayerPrefs.SetFloat("SfxVolume", value);

			UpdateSfxVolume();
		}
	}

	public static void UpdateMusicVolume()
	{
		GameObject musicSpeaker = GameObject.Find("MusicSpeaker");

		if (musicSpeaker != null)
		{
			musicSpeaker.GetComponent<AudioSource>().volume = MusicVolume;
		}
	}

	public static void UpdateSfxVolume()
	{
		GameObject speakers = GameObject.Find("Speakers");

		foreach (Transform speaker in speakers.transform)
		{
			if (speaker.name != "MusicSpeaker")
			{
				speaker.GetComponent<AudioSource>().volume = SfxVolume;
			}
		}
	}

	public static void Update()
	{
		UpdateMusicVolume();
		UpdateSfxVolume();
	}

	private Settings() {}
}
