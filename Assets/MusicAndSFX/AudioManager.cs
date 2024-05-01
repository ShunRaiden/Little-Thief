using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] public AudioSource musicSource;
	[SerializeField] public AudioSource SFXSource;

	[Header("Audio Clip")]
	public AudioClip backGround;
	public AudioClip backGroundIngame;
	public AudioClip backGroundAlert;
	public AudioClip hoverMouse;
	public AudioClip clickMouse;
	public AudioClip arrowChange;
	public AudioClip getItem;
	public AudioClip keyNum;
	public AudioClip enemyDetect;
	public AudioClip loseGame;
	public AudioClip openNumPad;
	public AudioClip openPaper;
	public AudioClip openDoor;
	public AudioClip sentQuest;
	public AudioClip winGame;


	public static AudioManager instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);

		}
		else
		{
			Destroy(gameObject);
		}

	}

	private void Start()
	{
		musicSource.clip = backGround;
		musicSource.Play();
	}

}
