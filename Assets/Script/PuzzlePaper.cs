using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePaper : MonoBehaviour
{
    [SerializeField] private GameObject paperUI;
	public GameObject pressE;
	public PauseMenu pauseMenu;

	private void Start()
	{
		
	}

	public void OnTriggerStay(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
			PlayerManager playerManager = other.GetComponent<PlayerManager>();

			pressE.SetActive(true);
			if (Input.GetKey(KeyCode.E))
			{
				if(playerManager.gamePause == false)
				{
					MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.openPaper);
				}
				paperUI.SetActive(true);
				MainGameController.instance.mouseOnOff(true);
				pauseMenu.isPause = true;
				playerManager.gamePause = true;

				

			}

			if (Input.GetKey(KeyCode.Escape) && playerManager.gamePause == true)
			{
				paperUI.SetActive(false);
				playerManager.gamePause = false;
			}
		}
	}

	public void OnTriggerExit(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
			PlayerManager playerManager = other.GetComponent<PlayerManager>();

			pressE.SetActive(false);
			paperUI.SetActive(false);
			MainGameController.instance.mouseOnOff(false);
			playerManager.gamePause = false;

		}
	}

	public void ExitPaper()
	{

		PlayerManager playerManager = FindObjectOfType<PlayerManager>();

		if (playerManager.gamePause == true)
		{
			MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.openPaper);
		}

		paperUI.SetActive(false);
		MainGameController.instance.mouseOnOff(false);
		pauseMenu.isPause=false;
		playerManager.gamePause = false;
	}
}
