using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
	[SerializeField] private Key.KeyType keyType;
	public GameObject[] allItem;
	public Key[] itemsNumber;
	public GameObject[] useItemNum;

	public Key.KeyType GetKeyType()
	{
		return keyType;
	}
	private void Update()
	{
		for(int i = 0; i < itemsNumber.Length; i++)
		{
			if(itemsNumber[i] == null)
			{
				allItem[i].SetActive(true);
			}
			if(useItemNum[i] == null)
			{
				allItem[i].SetActive(false);
			}
		}
	}

}
