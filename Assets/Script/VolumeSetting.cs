using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
	[SerializeField] private AudioMixer myMixer;
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider soundSlider;

	private void Start()
	{
		if (PlayerPrefs.HasKey("musicVolume"))
		{
			LoadVolume();
		}
		else
		{

			SetMusicVolume();
			SetSoundVolume();
		}

	}

	public void SetMusicVolume()
	{
		float volume = musicSlider.value;
		myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("musicVolume", volume);
	}

	public void SetSoundVolume()
	{
		float volume = soundSlider.value;
		myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("SFXVolume", volume);
	}



	public void LoadVolume()
	{
		musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
		soundSlider.value = PlayerPrefs.GetFloat("SFXVolume");

		SetMusicVolume();
		SetSoundVolume();
	}
}