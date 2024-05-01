using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;

	bool isNull=false;

	private void Awake()
	{
		keyList = new List<Key.KeyType>();
	}

	public void AddKey(Key.KeyType keyType)
	{		
		keyList.Add(keyType);
	}

	public void RemoveKey(Key.KeyType keyType)
	{
		keyList.Remove(keyType);
	}

	public bool ContainsKey(Key.KeyType keyType)
	{
		return keyList.Contains(keyType);

	}

	private void OnTriggerEnter(Collider other)
	{
		Key key = other.GetComponent<Key>();
		if(key != null)
		{
			if(isNull) return;
			MainGameController.instance.isAlert = true;
			MainGameController.instance.eventNum--;
			AddKey(key.GetKeyType());

			MainGameController.instance.audioManager.SFXSource.PlayOneShot(MainGameController.instance.audioManager.getItem);

			Destroy(key.gameObject);
			isNull = true;

		}

		KeyDoor keyDoor = other.GetComponent<KeyDoor>();
		if(keyDoor != null)
		{
			if (ContainsKey(keyDoor.GetKeyType()))
			{
				RemoveKey(keyDoor.GetKeyType());
				keyDoor.OpenDoor();
			}
		}
	}
}
