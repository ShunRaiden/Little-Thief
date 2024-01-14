using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGate : MonoBehaviour
{
	public GameObject endGameUI;

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerManager playerManager = other.GetComponent<PlayerManager>();

			MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.winGame);
			playerManager.gamePause = true;
			endGameUI.SetActive(true);
			MainGameController.instance.mouseOnOff(true);

			playerManager.gamePause = false;
		}
	}
}
