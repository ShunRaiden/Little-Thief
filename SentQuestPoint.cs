using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentQuestPoint : MonoBehaviour
{
	public GameObject blockExit;

	private void Update()
	{
		if(MainGameController.instance.canExit == true)
		{
			Destroy(blockExit);
		}
	}
	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "MainQuest")
		{
			Destroy(other.gameObject);
			MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.sentQuest);
			MainGameController.instance.eventNum--;
			MainGameController.instance.questDone++;
		}
	}
}
