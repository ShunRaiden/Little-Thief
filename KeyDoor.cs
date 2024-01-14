using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;
	public int doorNum;
	public Animator animDoor;
	public GameObject doorBlock;

	private void Start()
	{
		animDoor.ResetTrigger("OpenDoor");
		
	}

	public Key.KeyType GetKeyType()
	{
		return keyType;
	}

	public void OpenDoor()
	{
		PlayerManager playerManager = FindObjectOfType<PlayerManager>();

		MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.openDoor);
		animDoor.SetTrigger("OpenDoor");
		Destroy(doorBlock);
	}
}
