using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
		audioManager = FindObjectOfType<AudioManager>();
    }

	public void HoverSound()
	{

		audioManager.SFXSource.PlayOneShot(audioManager.hoverMouse);
	}

	public void clickSound(int clickOrArrow)
	{
		if (clickOrArrow == 0)
		{
			audioManager.SFXSource.PlayOneShot(audioManager.clickMouse);
		}
		if (clickOrArrow == 1)
		{
			audioManager.SFXSource.PlayOneShot(audioManager.arrowChange);
		}

	}
}
