using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXBYPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _walk, _run,_Jump;

	[Header("Plyaer Audio Clip")]
	public AudioClip walking_Poon;
	public AudioClip run_Wood;
	public AudioClip jumping_Wood;

	public void sfxPlayerInput(float snappedY,bool isJump)
	{
		if (isJump)
		{
			_run.enabled = false;
			_walk.enabled = false;
			_Jump.enabled = true;

		}


		if (!isJump)
		{
			_Jump.enabled = false;

			if (snappedY <= 0.5)
			{
				_run.enabled = false;
				_walk.enabled = false;
			}
			else if (snappedY > 0.5 && snappedY <= 1)
			{
				_run.enabled = false;
				_walk.enabled = true;
			}
			else if (snappedY > 1 && snappedY <= 2)
			{
				_run.enabled = true;
				_walk.enabled = false;
			}
			else
			{
				_run.enabled = false;
				_walk.enabled = false;
			}
		}
		
	}


}
