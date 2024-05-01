using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
	public GameObject[] ChaCamUI;

	private void Start()
	{
		if(!PlayerPrefs.HasKey("CharacterSelection"))
		{
			PlayerPrefs.SetInt("CharacterSelection", 0);
		}

		for(int i = 0; i < ChaCamUI.Length; i++)
		{
			if(PlayerPrefs.GetInt("CharacterSelection") == i)
			{
				ChaCamUI[i].SetActive(true);
			}
			else
			{
				ChaCamUI[i].SetActive(false);
			}
		}
		
	}

	public void CharSelected(int chaNum)
	{
		PlayerPrefs.SetInt("CharacterSelection", chaNum);
		PlayerPrefs.Save();
	}

}
